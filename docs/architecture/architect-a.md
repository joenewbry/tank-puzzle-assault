# Tank Puzzle Assault — Architecture Proposal A

Author: Architect Candidate A (`tech-architect`)  
Date: 2026-03-03  
Target: Unity multiplayer puzzle-action game (Web + mobile + Xbox)

---

## 1) Architectural Intent

Build a **fast-iteration Unity project** where the tank’s gun uses a **fixed firing arc (no manual tilt)** and puzzle-solving comes from positioning, timing, and shot type—not aim elevation control.

This proposal prioritizes:
- short iteration loops for level designers,
- stable cross-platform multiplayer,
- low complexity for initial release,
- clear upgrade path to production-scale backend.

---

## 2) Core Technical Choices (Concrete)

## Engine and packages
- **Unity 2022.3 LTS** (long-term support stability).
- **URP** for consistent visuals on WebGL/mobile/Xbox with scalable quality tiers.
- **Input System package** (single action map asset for keyboard/gamepad/touch).
- **Netcode for GameObjects (NGO)** + **Unity Transport**.
- **UGS Lobby + Relay** for quick host/client matchmaking without dedicated servers in MVP.
- **Addressables** for map/prefab/content streaming and memory control.
- **Cinemachine** for dynamic follow/zoom/cinematic boss intro.

## Asset pipeline
Use existing package as base art/gameplay prefabs:
- `/Users/joe/dev/openarcade/tank-royale/assets/packs/assethunts_gamedev_starter_kit_tanks_v100.unitypackage`

Import strategy:
1. Import into `Assets/_ThirdParty/StarterKitTanks/`.
2. Wrap imported tank prefabs with local gameplay/network prefabs under `Assets/Game/Prefabs/Tanks/`.
3. Never edit third-party prefabs directly; use prefab variants and adapter scripts.

---

## 3) High-Level System Diagram (Text)

```text
[Client: Web/Mobile/Xbox]
  |  Input (Input System)
  v
[Player Controller]
  |-- movement intent ---------------------------->
  |-- fire request ------------------------------->
  v                                                
[Netcode Client RPC] ---------------------> [Host Authority (NGO Host)]
                                               |
                                               | validates cooldown, ammo, LOS rules
                                               v
                                      [Combat & Puzzle Simulation]
                                        |   |   |   |   |
                                        |   |   |   |   +--> [PowerUp System]
                                        |   |   |   +------> [Destructible System]
                                        |   |   +----------> [Ramp/Traversal System]
                                        |   +--------------> [Enemy AI System]
                                        +------------------> [Match State + Objectives]
                                               |
                                 [NetworkVariables / RPC snapshots]
                                               |
                                               v
                                 [All Clients: VFX/SFX/UI Feedback]

[Map Progression]
  [World Node Map UI] <-> [Save Service (Local JSON + optional Cloud Save)]
```

Authority model: **host-authoritative simulation** for puzzle state, destructibles, pickups, and enemy AI.

---

## 4) Gameplay Systems Architecture

## 4.1 Fixed-Arc Shooting (No Tilt)
- Turret yaw allowed; elevation fixed by weapon profile.
- Each weapon has ScriptableObject fields:
  - `muzzleVelocity`
  - `gravityScale`
  - `maxBounces`
  - `explosionRadius`
  - `damageVsArmorType`
- Client shows predicted arc spline for accessibility, but host resolves final hit.

Implementation notes:
- Use deterministic-ish ray-step ballistic sampling (fixed tick interval on host).
- All puzzle triggers consume **host-confirmed** hit events only.

## 4.2 Destructible Objects
- `DestructibleObject` with:
  - health,
  - armor tag (wood/stone/metal/explosive),
  - networked state (`Intact`, `Cracked`, `Destroyed`).
- Destruction can reveal ramps, open lanes, drop pickups, trigger switches.
- Debris is client-only pooled VFX (not networked rigidbodies for performance).

## 4.3 Ramp Tiles and Traversal Obstacles
- Grid-authored ramp tiles with directional metadata.
- Tank movement validates slope angle and traction profile.
- Puzzle gates combine ramps + breakable blockers + timing doors.

## 4.4 Health Boxes and Mystery Boxes
- `PickupSpawner` (networked) with respawn policy per mode.
- Health Box: instant repair up to cap.
- Mystery Box: weighted random power-up:
  - Shield bubble,
  - Ricochet rounds,
  - EMP shot,
  - Turbo treads,
  - Mine deploy.
- RNG seeded by match seed and resolved on host.

## 4.5 Tank Roster
Player choices:
- **Blue Tank**: balanced mobility/armor.
- **Green Tank**: faster traversal, lighter armor.

Enemies (red variants):
1. **Red Scout**: flank + harass behavior.
2. **Red Bruiser**: slow, high HP, area shell.
3. **Red Sniper**: long-range line control, lower HP.
4. **Final Boss Tank**: phase-based behavior (shield phase, summon phase, rage phase).

AI architecture:
- Hybrid **FSM + utility scoring**.
- Shared blackboard (target, cover node, objective node, cooldowns).
- NavMesh for movement; puzzle interaction uses explicit action points.

---

## 5) Multiplayer / Networking Approach

## Session model
- Match size: **2–4 players co-op/versus hybrid** (MVP target: 2-player co-op + AI enemies).
- One player is host; others connect via Relay.
- No host migration in MVP (match ends gracefully on host disconnect).

## Sync strategy
- NetworkObject for tanks, enemies, pickups, dynamic puzzle entities.
- `NetworkVariable` for compact replicated state:
  - HP, ammo, active power-up, objective flags.
- RPCs for transient events:
  - fire command,
  - damage event,
  - pickup consumed,
  - objective complete.

## Anti-cheat and correctness
- Host validates fire rate, movement speed, pickup claims.
- Client prediction limited to visual smoothing; no client authority on objective state.

## Platform considerations
- WebGL: lower tick rate profile (20 Hz) + reduced VFX budget.
- Mobile: adaptive quality + simplified shadows.
- Xbox: full quality tier, gamepad-first UX, certification-safe pause/network handling.

---

## 6) Maps and Difficulty Escalation

Three maps, each with 5 puzzle-combat stages (15 total nodes):

1. **Map 1: Rustyard Outskirts (Intro)**
   - Teaches fixed-arc fundamentals.
   - Basic destructibles, single-ramp puzzles.
   - Enemy focus: Red Scout.

2. **Map 2: Quarry Switchbacks (Intermediate)**
   - Multi-step puzzles with chain destruction.
   - Mixed ramp traversal and timed gates.
   - Enemy mix: Scout + Bruiser + Sniper.

3. **Map 3: Siege Foundry (Advanced + Boss)**
   - Dense hazard layout, constrained ammo puzzles.
   - Multi-lane objectives under pressure.
   - Final boss tank on Stage 15.

Difficulty knobs per stage:
- allowed retries,
- enemy accuracy,
- puzzle timer windows,
- pickup spawn frequency,
- destructible durability.

---

## 7) Save + Progression Map (Candy Crush-style Node Map)

## Node map UX
- World map screen shows connected level nodes with star ratings.
- Unlock rule: complete previous node with at least 1 star.
- 3 stars awarded per node:
  1) complete objective,
  2) finish under par time,
  3) no team wipe.

```text
Map 1: Rustyard
(1)---(2)---(3)---(4)---(5*)
                    \
Map 2: Quarry         (6)---(7)---(8)---(9)---(10*)
                                 \
Map 3: Foundry                     (11)--(12)--(13)--(14)--(15 BOSS)
```

## Save schema
Saved locally as JSON (`Application.persistentDataPath`) with optional Cloud Save sync:
- player profile,
- unlocked nodes,
- per-node stars,
- selected tank (blue/green),
- settings/preferences,
- last checkpoint for resumed sessions.

Conflict policy: newest timestamp wins for MVP.

---

## 8) Settings, Key Bindings, Audio, and VFX Architecture

## Settings menu
- Graphics preset: Low/Medium/High/Auto.
- Master/Music/SFX sliders via `AudioMixer` exposed params.
- Gameplay toggles: aim-assist arc visibility, camera shake intensity.
- Network diagnostics toggle (ping/fps overlay).

## Key bindings / controls
- Input System action maps:
  - `Gameplay` (Move, Aim, Fire, Ability, Interact, Pause)
  - `UI` (Navigate, Submit, Cancel)
- Rebind UI stored per profile.
- Platform-specific defaults:
  - Web/PC keyboard + mouse,
  - Mobile dual-stick touch overlay,
  - Xbox gamepad layout with vibration profile.

## Sound architecture
- `AudioEventCatalog` ScriptableObjects map gameplay events to clips/variations.
- 2D UI bus + 3D world bus + ducking rules for VO/alerts.
- Pool-based one-shot audio emitters for high-frequency impacts.

## VFX architecture
- Particle prefabs by event type: muzzle, impact, destruction, power-up pickup.
- GPU-friendly variants for Web/mobile.
- `VFXRouter` listens to combat/puzzle events and spawns pooled effects.

---

## 9) Recommended Package / Folder Structure

```text
Assets/
  _ThirdParty/
    StarterKitTanks/                       # imported unitypackage content (read-only)
  Game/
    Art/
      Materials/
      VFX/
      UI/
    Audio/
      Mixer/
      Clips/
      Events/
    Prefabs/
      Tanks/
      Enemies/
      Puzzle/
      Pickups/
      UI/
    Scenes/
      Bootstrap.unity
      MainMenu.unity
      WorldMap.unity
      Map1_Stages.unity
      Map2_Stages.unity
      Map3_Stages.unity
    Scripts/
      Core/
      Combat/
      Puzzle/
      AI/
      Networking/
      UI/
      Progression/
      Save/
    Data/
      ScriptableObjects/
        Weapons/
        Tanks/
        Enemies/
        Pickups/
        Levels/
  AddressablesData/
  Settings/
```

---

## 10) Risks and Mitigations

1. **WebGL networking constraints / latency spikes**
   - Mitigation: lower tick profile, bandwidth budget tests early, simplify replicated objects.

2. **Host-authority fairness concerns**
   - Mitigation: keep competitive scoring light in MVP; focus on co-op puzzle progression.

3. **Cross-platform input complexity (touch vs gamepad vs keyboard)**
   - Mitigation: single Input System abstraction + platform-specific control adapters.

4. **Physics divergence across platforms**
   - Mitigation: host-resolved hit logic, avoid client-trusted physics outcomes.

5. **Scope creep from puzzle content creation**
   - Mitigation: reusable puzzle modules (destructible gate, pressure switch, ramp chain) and strict 15-node launch target.

6. **Asset package dependency risk**
   - Mitigation: wrap third-party assets through local variants and adapters; no direct edits.

7. **Xbox certification and service requirements**
   - Mitigation: maintain abstraction around online/session layer; certification checklist from vertical slice onward.

---

## 11) Phased Build Plan (Fast Iteration)

## Phase 0 — Foundation (Week 1)
- Unity project setup, URP, Input System, NGO + Relay skeleton.
- Import starter kit package and create local prefab variants.
- Create Bootstrap/MainMenu/WorldMap scenes.

## Phase 1 — Core Loop Prototype (Weeks 2–3)
- Fixed-arc firing, destructibles, ramps, health/mystery pickups.
- Blue/Green player tanks integrated.
- Single test arena with 2-player host/client.

## Phase 2 — Enemy + Puzzle Vertical Slice (Weeks 4–5)
- Red Scout/Bruiser/Sniper AI.
- 5 stages of Map 1 with star scoring and node progression.
- Settings menu + key rebinding + baseline audio/VFX.

## Phase 3 — Content Expansion (Weeks 6–8)
- Map 2 and Map 3 stage implementations.
- Boss tank (phase logic + presentation).
- Performance passes for WebGL/mobile.

## Phase 4 — Platform Hardening (Weeks 9–10)
- Xbox input polish and compliance prep.
- Match stability, reconnect/error flows.
- Save/cloud sync edge cases.

## Phase 5 — Pre-Launch QA (Weeks 11–12)
- Balance tuning, telemetry review, accessibility adjustments.
- Regression suite for puzzle/network interactions.
- Release candidate.

---

## 12) Definition of Done for This Architecture

- Supports required game concept (fixed-arc puzzle shooting).
- Includes destructibles, ramps, health/mystery boxes, power-ups.
- Includes blue/green player tanks, 3 red variants, boss tank.
- Defines 3-map escalation and multiplayer model.
- Covers Web + mobile + Xbox constraints.
- Specifies settings, key binding, sound, and VFX architecture.
- Provides concrete folder structure, risk list, progression map, and phased plan.
