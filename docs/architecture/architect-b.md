# Tank Puzzle Assault — Architecture Proposal B (Deterministic Gameplay Islands)

## 1) Architecture Overview

### Positioning (how this differs from a typical approach)
Most Unity multiplayer tank games default to **continuous Rigidbody physics + NetworkTransform replication** and then patch desync with interpolation.

**Proposal B intentionally avoids that as the gameplay authority.**

Instead, we use a **hybrid architecture**:
- **Authoritative server simulation** for gameplay-critical systems (movement, aiming, shots, puzzle destruction, AI decisions).
- **Deterministic gameplay islands** running at fixed tick (30 Hz server / 60 Hz local presentation).
- **Client-side presentation physics only** for non-authoritative debris and cosmetic effects.

This trades some implementation complexity for:
- Better cross-platform consistency (Web/mobile/Xbox)
- Lower replication bandwidth than full transform sync
- Cleaner handling of puzzle logic and fixed-arc projectile behavior

### High-level Runtime Model
- Networking model: **Server authoritative with client prediction/reconciliation** for local player movement.
- Tick model:
  - Server simulation tick: `33.33ms`
  - Input collection window: per tick
  - Snapshot replication: 10–20 Hz delta snapshots
- Coordinates and logic:
  - Gameplay core uses **quantized fixed-point/int domain** for deterministic checks.
  - Unity world transforms are projection of authoritative state.

### Core Gameplay Fit
- **Fixed-arc shooting (no tilt):** projectile arc solved from fixed launch profile table, not live turret tilt.
- **Destructible puzzles:** puzzle objects are state machines with deterministic thresholds and event IDs.
- **Ramps and routing:** authored ramp segments and route graph edges, not unconstrained rigidbody traversal.
- **Classes and enemies:** Blue/Green player archetypes, Red enemy variants, and boss with phased state machine.

---

## 2) Subsystem Boundaries

| Subsystem | Responsibilities | Owns Authority? | Inputs | Outputs |
|---|---|---:|---|---|
| Session & Match Service | Lobby, room setup, map/difficulty selection, role assignment (Blue/Green) | Yes (server) | Player join/leave, party data | Match config, spawn seeds |
| Input Pipeline | Input sampling, command packing, sequence IDs, local prediction | Client (authoritative on local intent only) | Device input (KBM/gamepad/touch) | Command stream |
| Gameplay Core (Deterministic Island) | Tank movement state, fire commands, projectile sim, hit/puzzle resolution | Yes (server) | Commands + map data + RNG seed | Authoritative state events |
| Puzzle Runtime | Destructible object health/state, triggers, chain reactions, objective checks | Yes (server) | Damage events, trigger events | Puzzle state transitions |
| AI Director + Agents | Enemy spawn pacing, red variants behavior, boss phases | Yes (server) | Player state, puzzle state, nav graph | AI commands, spawn/despawn events |
| Route/Navigation Graph | Obstacle routing, ramp validity, dynamic blocked edges | Yes (server) | Map topology, destructible state | Path requests/responses |
| Replication Layer | Delta snapshots, event stream, interest management | Yes (server) | Authoritative state | Client snapshots/events |
| Presentation Layer | Animation, VFX, audio, camera, cosmetic debris | Client | Snapshot/event stream | Frame visuals/sfx |
| Settings & Bindings | Keybinds, controller/touch presets, audio/video options | Client | User prefs | Runtime config |
| Telemetry & Replay | Match metrics, deterministic event log, replay validation | Server + tools | Event stream | Dashboards/replay files |

### Key boundary rule
If a system can change win/loss, objective progress, or combat outcomes, it is in the **authoritative deterministic island**.

---

## 3) Deterministic Gameplay Concerns

### Determinism Strategy
We do **deterministic core + non-deterministic shell**:
- Deterministic core includes:
  - Movement intent resolution
  - Firing timing/cooldowns
  - Projectile path stepping
  - Damage and destruction
  - AI tactical decisions
- Non-deterministic shell includes:
  - Particle physics
  - Debris simulation
  - Camera shake and blend trees

### Fixed-arc shooting without turret tilt
- Turret visual remains level/no tilt.
- Each weapon class has a pre-authored `ArcProfile`:
  - launch speed
  - gravity scalar
  - max range buckets
  - collision radius
- Server advances projectile by fixed tick integration in quantized space.
- Client renders spline approximation from server seed/event ID for visual smoothness.

### Physics divergence mitigation
Avoid relying on cross-platform Rigidbody outcomes for gameplay:
- Use kinematic/capsule sweep checks + deterministic collision queries in authoritative sim.
- Treat Unity PhysX interactions as **visual-only** unless explicitly mirrored in deterministic core.
- Ramps represented as authored segments with deterministic elevation function.

### RNG and event ordering
- Single match seed generated server-side.
- Sub-seeds per subsystem (`AI`, `Loot`, `Ambient`).
- All gameplay events carry monotonic `EventId` and `TickId`.
- Replay and desync debugging based on event log hash every N ticks.

### Netcode implications
- Client sends command packets (`InputSeq`, `Tick`, `Actions`).
- Server returns:
  - periodic state snapshots
  - guaranteed event channel for critical events (destroyed wall, objective trigger, boss phase)
- Reconciliation:
  - local player corrected if divergence > threshold
  - remote players interpolated/extrapolated conservatively

---

## 4) AI State Architecture

### AI Model: Hierarchical Statecharts + Utility Scoring
Instead of pure Behavior Trees for all logic, use:
1. **Global AI Director** (encounter pacing, spawn budget, pressure score)
2. **Per-agent Statechart** (clear discrete states)
3. **Utility selector** inside combat states for action choice

This is chosen for predictable multiplayer debugging and cleaner boss-phase control.

### Enemy families
- **Red Scout:** high mobility, flanks, low armor
- **Red Bruiser:** slower, pushes choke points, high HP
- **Red Artillery:** long-range arc shots, puzzle denial
- **Boss Tank:** multi-phase with arena interaction hooks

### Agent statechart (example)
- `Spawn`
- `AcquireTarget`
- `RouteToAdvantage`
- `Engage` (utility picks fire, strafe, retreat, hold angle)
- `UsePuzzleInteraction` (shoot weak wall, trigger bridge, block path)
- `Fallback`
- `Disabled/Destroyed`

Transitions are driven by deterministic blackboard facts:
- line-of-fire quality
- path cost delta
- incoming threat score
- objective urgency
- current map difficulty policy

### Boss architecture
Boss uses **phase state machine** with deterministic triggers:
- Phase 1: area denial shells
- Phase 2: deploy minions + break cover nodes
- Phase 3: aggressive routing + charge pattern
- Enrage: timer/HP threshold

All phase transitions are server-authored events for synchronization.

---

## 5) Content Pipeline

### Source assets
Starter kit source:
`/Users/joe/dev/openarcade/tank-royale/assets/packs/assethunts_gamedev_starter_kit_tanks_v100.unitypackage`

### Import and normalization flow
1. Import package into `Assets/ThirdParty/TankStarterKit/`.
2. Create **wrapper prefabs** in `Assets/Game/Prefabs/...` (do not edit vendor prefabs directly).
3. Attach gameplay metadata via ScriptableObjects:
   - `TankClassDefinition` (Blue/Green/Red/Boss stats)
   - `WeaponArcProfile`
   - `PuzzleNodeDefinition`
   - `MapDifficultyProfile`
4. Bake map topology into deterministic data assets:
   - route graph
   - ramp segments
   - destructible node registry
5. Build addressable groups per platform quality tier.

### Map and difficulty authoring
Create 3 maps, each with policy overlays:
- **Map 1 (Easy):** fewer branching routes, limited destructibles
- **Map 2 (Medium):** mixed ramps, more puzzle dependencies
- **Map 3 (Hard):** dense routing, boss arena interactions

Difficulty should be data-driven via `MapDifficultyProfile`:
- enemy spawn curves
- AI aggressiveness multipliers
- puzzle tolerance windows
- resource/ammo frequency

### Settings/keybind/audio/VFX systems
- Settings persisted as versioned JSON profile.
- Input abstraction layer supports:
  - Keyboard/Mouse (Web/Xbox via browser optional)
  - Gamepad (mobile controllers + Xbox)
  - Touch virtual controls (mobile)
- Audio bus layout:
  - Master / Music / SFX / VO / UI
- VFX budget tags per effect (`Low`, `Med`, `High`) to enable platform scaling.

---

## 6) Performance Strategy by Platform

### Shared budgets (target)
- Simulation: <= 4 ms/frame server-side equivalent
- Rendering:
  - gameplay camera total draw calls <= 250 (mobile), <= 500 (web), <= 900 (Xbox)
- GC allocations during gameplay: near-zero (pool all projectiles/VFX)
- Network budget: ~20–40 KB/s per client average in 4–8 player sessions

### Web (WebGL)
- Target 30–60 FPS depending device class.
- Aggressive texture compression and atlas usage.
- Limit shader variants and post-processing.
- Use lower snapshot frequency + stronger interpolation to reduce bandwidth spikes.
- Keep memory footprint tight; avoid runtime asset churn.

### Mobile (iOS/Android)
- Default 30 FPS with optional 60 FPS mode.
- Dynamic resolution scaler + adaptive VFX quality.
- Touch input assist (auto-aim cone and fire buffering).
- Use GPU instancing for repeated environment props.
- Thermal fallback profile after sustained load.

### Xbox
- Target locked 60 FPS.
- Higher-quality shadows/particles and larger visibility radius.
- Retain deterministic core identical to other platforms.
- Use higher-frequency snapshots in competitive modes (if bandwidth allows).

### Multiplayer stability/perf safeguards
- Interest management by spatial sectors and combat relevance.
- Event coalescing for repeated non-critical updates.
- Server-side anti-cheat checks on impossible movement/fire cadence.
- Built-in desync diagnostics: periodic state hash from clients in debug/test builds.

---

## 7) Risks and Tradeoffs

### Benefits
- More reliable cross-platform gameplay consistency than transform-sync heavy model.
- Better replay/debuggability via deterministic event stream.
- Cleaner puzzle and boss synchronization in multiplayer.

### Costs
- Higher upfront engineering complexity.
- Requires strict discipline around gameplay-vs-presentation separation.
- Authoring tools needed for route/ramp/puzzle deterministic data baking.

### Mitigation
- Start with one vertical-slice map and one enemy family.
- Build deterministic validation tooling early (state hash, replay compare).
- Keep fallback mode: if deterministic subsystem unavailable, run authoritative coarse sim with reduced feature set for internal testing only.

---

## 8) Recommended Implementation Sequence
1. Build deterministic tank movement + fixed-arc projectile core.
2. Add puzzle node state machine and replication events.
3. Integrate Blue/Green classes and first Red variant.
4. Implement AI Director + statechart baseline.
5. Ship Map 1 vertical slice (all target platforms).
6. Extend to Map 2/3 + boss phases + full settings/VFX scalability.

This sequence de-risks core synchronization before content scale-up.
