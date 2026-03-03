# Tank Puzzle Assault — Architecture Proposal C (Ship-Fast, Extend-Clean)

## 1) Executive Summary

**Objective:** deliver a playable, monetizable-quality core quickly (Web + mobile first), while keeping a clean path to Xbox and richer live operations.

**Strategy:** use a **modular monolith** in Unity with strict feature boundaries, data-driven balancing, and a lightweight online stack that can evolve from host-based sessions to dedicated servers without rewriting gameplay.

**Why this is Candidate C:** fastest practical route to "fun + stable" while deliberately preserving extension seams (interfaces, ScriptableObject configs, service adapters).

---

## 2) Product & Technical Constraints

- Unity tank puzzle-action game with **fixed ballistic arc** (no tank tilt mechanic)
- Gameplay entities: destructibles, ramps, health/mystery boxes, enemy red variants, boss tank
- Player tank selection: blue or green
- 3 maps with escalating difficulty
- AI + multiplayer
- Platforms: Web, mobile, Xbox
- Full shell UX: menu, settings, keybinds, sound, VFX
- Reuse starter assets from:
  `/Users/joe/dev/openarcade/tank-royale/assets/packs/assethunts_gamedev_starter_kit_tanks_v100.unitypackage`

---

## 3) Architecture Principles (optimized for shipping speed)

1. **Single Unity project, modular folders/asmdefs** (not microservices).
2. **Data-first tuning** via ScriptableObjects + JSON overrides for live balancing.
3. **Gameplay-authoritative simulation layer** abstracted from transport.
4. **MVP-first order:** offline core loop → host multiplayer → Xbox hardening.
5. **Asset-pack leverage over custom art** for speed.
6. **Contract seams everywhere:** interfaces for networking, economy/events, persistence.

---

## 4) Architecture Diagram (text)

```text
+--------------------------------------------------------------------------------+
|                                Tank Puzzle Assault                             |
+--------------------------------------------------------------------------------+
|                                  Presentation                                  |
|  UI: Main Menu / HUD / Pause / Settings / Rebind / Match Results              |
|  VFX/SFX: Audio Mixer, pooled VFX prefabs, platform quality profiles           |
+--------------------------------------------------------------------------------+
|                                Game Application                                |
|  GameFlowController  SessionController  ProgressionController  SaveProfile     |
+--------------------------------------------------------------------------------+
|                                 Domain Modules                                 |
|  Combat      AI        Puzzle/Level      Items/Powerups      Destruction       |
|  (ballistic  (FSM+BT)  (ramps/triggers)  (health/mystery)    (chunk states)    |
|   solver)                                                                    |
+--------------------------------------------------------------------------------+
|                             Multiplayer & Services                             |
|  INetworkRuntime (adapter) -> NGO + UGS Lobby/Relay (MVP)                     |
|  Future adapter: dedicated server runtime (Multiplay or custom headless)       |
|  Telemetry/EventBus  RemoteConfig  CloudSave profile                           |
+--------------------------------------------------------------------------------+
|                           Data & Content Pipeline                              |
|  ScriptableObjects: tanks, weapons, enemy archetypes, map rules, loot tables   |
|  Addressables: maps, VFX variants, cosmetics, localized UI assets              |
+--------------------------------------------------------------------------------+
|                              Platform Abstraction                              |
|  Input System (KBM/Touch/Gamepad)   Build Profiles (Web/Mobile/Xbox)           |
+--------------------------------------------------------------------------------+
```

---

## 5) Unity Project Layout (proposed)

```text
Assets/
  _TPA/
    Core/                 # bootstrap, service locator/DI-lite, shared utils
    Gameplay/
      Combat/
      AI/
      Puzzle/
      Destruction/
      Powerups/
    Multiplayer/
      Runtime/
      Adapters/NGO/
      Adapters/DedicatedFuture/
    UI/
      Menu/
      HUD/
      Settings/
    Audio/
    VFX/
    Maps/
      Map01_Training/
      Map02_Fortress/
      Map03_BossBasin/
    Data/
      ScriptableObjects/
      RemoteDefaults/
    Integrations/
      UGS/
    Tests/
      EditMode/
      PlayMode/
```

Use assembly definitions per major module to keep compile times low and boundaries clean.

---

## 6) Core Gameplay Architecture

### 6.1 Fixed-arc aiming (no tilt)
- Tank body remains level relative to terrain (visual suspension only).
- Turret controls:
  - **Yaw only** for aiming direction.
  - Elevation angle is fixed per weapon profile (e.g., 35° default).
- Player chooses shot **power** (hold/release or slider), producing puzzle-style trajectory control.
- Optional trajectory preview uses sampled ballistic points from the same solver used by runtime simulation.

### 6.2 Destruction and puzzle surfaces
- Hybrid destruction for speed:
  - **State-based breakables** (intact/damaged/destroyed meshes) for most objects.
  - Limited physics fragments only on high-value hero objects.
- Ramps and ricochet surfaces are explicit colliders with material tags to drive bounce and friction behavior.

### 6.3 Tanks and faction variants
- Player selectable prefabs: `Tank_Blue_Player`, `Tank_Green_Player`.
- Enemy archetypes (red): Scout, Artillery, Bruiser, Boss.
- Differences mostly data-driven (HP, move speed, fire cadence, ability set).

### 6.4 Power-up system
- `PowerupDefinition` ScriptableObject + runtime `IPowerupEffect` interface.
- Initial meaningful pool:
  - Repair Kit (instant HP)
  - Reactive Armor (temporary damage reduction)
  - Overcharge Shot (next shot +AOE)
  - Ricochet Master (extra bounce count)
  - EMP Round (brief enemy disable)

---

## 7) Network & Session Model

### 7.1 MVP network model (ship-fast)
- **Host-authoritative multiplayer** using Unity Netcode for GameObjects (NGO).
- **UGS Lobby + Relay** for matchmaking/session bootstrap.
- Match topology:
  - 1 host peer (authoritative simulation)
  - 1–3 clients (configurable by mode/platform limits)
- Works for Web/mobile quickly with minimal backend engineering.

### Session flow
1. Anonymous/sign-in auth bootstrap.
2. Player chooses tank color/loadout.
3. Create/join lobby.
4. Host starts match; Relay allocation distributed.
5. Scene sync to selected map.
6. In-match state sync (tank transforms, projectiles, damage events, pickups).
7. End-of-match results + profile progression save.

### Authority model
- Host validates shot fire requests, power values, hit/damage outcomes.
- Clients send input commands only.
- Anti-cheat MVP: server-side sanity checks (rate limits, max power thresholds, cooldown gating).

### 7.2 Extensibility path
- Network calls wrapped by `INetworkRuntime`, `IMatchSessionService`, `IPlayerPresenceService`.
- Future migration to dedicated servers replaces adapter layer, not gameplay domain logic.

---

## 8) Map Progression System (3-map escalation)

### Structure
- `MapDefinition` ScriptableObject fields:
  - map id, biome theme, objective type
  - enemy wave schedule
  - puzzle constraints (bounce zones, destructible gates)
  - reward tables
  - unlock criteria

### Proposed progression
1. **Map 01: Training Range**
   - Teaches fixed-arc + power control + simple destructibles.
   - Enemy mix: Scout + Bruiser intro.
2. **Map 02: Red Quarry Fortress**
   - Ramps, layered cover, mystery boxes with risk/reward.
   - Enemy mix includes Artillery; timed pressure objective.
3. **Map 03: Boss Basin**
   - Multi-phase boss tank + elite escorts.
   - Requires mastery of ricochet, ramp angles, and power-up timing.

### Difficulty scaling
- Scalar bundle per map (`DifficultyProfile`): enemy accuracy, HP multiplier, pickup spawn cadence, puzzle timer windows.
- Supports solo and multiplayer scaling via player-count coefficients.

---

## 9) Menu / Settings / Keybind / Sound / VFX Stack

- **UI framework:** Unity UI Toolkit for shell/menu + uGUI for gameplay HUD where faster.
- **Input:** Unity Input System action maps:
  - Gameplay (move, aim yaw, charge fire, utility)
  - UI navigation
  - Platform-specific schemes (KBM, touch virtual sticks, gamepad)
- **Rebinds:** persistent per profile via JSON save + Cloud Save sync fallback.
- **Audio stack:**
  - Mixer groups: Master, Music, SFX, UI, Voice
  - Ducking snapshots for pause/menu
  - Low-latency one-shot pooling for cannon impacts
- **VFX stack:**
  - Pooling for muzzle flash, explosions, debris
  - Platform quality tiers (Web low/med, mobile med/high, Xbox high)

---

## 10) Live-Ops Hooks (from day one, lightweight)

1. **Telemetry event bus** (`IGameEventSink`) with schema versioning.
2. **Remote Config keys** for non-code tuning:
   - enemy HP/damage multipliers
   - loot drop weights
   - map unlock thresholds
3. **Daily/weekly challenge hooks** (disabled by default in prototype).
4. **Feature flags** for experimental power-ups/modes.
5. **Addressables labels** for content bundles (future seasonal map variants).

MVP behavior if services unavailable: fail gracefully to embedded defaults.

---

## 11) Test Strategy

### 11.1 Automated
- **EditMode tests**
  - Ballistic solver correctness (golden trajectories)
  - Damage and power-up stacking rules
  - Progression unlock logic
- **PlayMode tests**
  - Map objective completion and wave triggers
  - Pickup spawn/consume lifecycle
  - Host/client sync sanity for fire-hit-damage loop
- **Contract tests** for service adapters (`INetworkRuntime`, `IConfigService`).

### 11.2 Manual matrix
- Platforms: WebGL (Chrome/Edge), Android/iOS representative devices, Xbox target kit.
- Control schemes: KBM, touch, gamepad.
- Scenarios: reconnect, host migration fallback (if host quit -> match end in MVP), low FPS stability.

### 11.3 Performance budgets (initial)
- Web: 60fps target on mid desktop, playable 30fps floor.
- Mobile: stable 30fps on target mid-tier devices.
- Xbox: 60fps target with higher VFX tier.

---

## 12) Migration Plan — Prototype to Production

### Phase 0 (Week 1–2): Fast Prototype
- Import starter kit package and stand up graybox map.
- Implement fixed-arc combat loop, basic enemy AI, one power-up.
- Local single-player only.

### Phase 1 (Week 3–5): Vertical Slice
- Complete Map 01 + Map 02.
- Add host multiplayer (Lobby + Relay + NGO adapter).
- Deliver full menu/settings/rebind/audio baseline.
- Start telemetry + remote config keys.

### Phase 2 (Week 6–8): Content Complete
- Add Map 03 boss encounter.
- Expand enemy variants and power-up set.
- Platform optimization pass (Web/mobile), initial Xbox compatibility pass.

### Phase 3 (Week 9+): Production Hardening
- Improve net robustness, anti-cheat checks, disconnect handling.
- Optional dedicated server adapter for competitive modes.
- Live-ops cadence: rotating challenges, balance hotfixes, content bundles.

### Migration safeguards
- Keep simulation deterministic-ish where practical (fixed tick + bounded randomness seeds).
- Maintain adapter interfaces to avoid domain rewrites.
- Enforce content/data schema versioning for save compatibility.

---

## 13) Starter Asset Package Integration Plan

Use package:  
`/Users/joe/dev/openarcade/tank-royale/assets/packs/assethunts_gamedev_starter_kit_tanks_v100.unitypackage`

Integration steps:
1. Import into `Assets/ThirdParty/AssetHunts_Tanks/`.
2. Wrap third-party prefabs with local gameplay components (do not modify vendor assets directly).
3. Create adapter prefabs in `Assets/_TPA/Gameplay/*/Prefabs`.
4. Extract reusable materials/sounds into Addressables groups.
5. Track vendor asset updates via separate changelog file.

---

## 14) Key Risks and Mitigations

- **Risk:** Web + multiplayer performance variance.  
  **Mitigation:** strict pooling, simplified destruction, lower sync frequency for non-critical entities.
- **Risk:** Host advantage/cheat potential in MVP.  
  **Mitigation:** clamp + validate combat commands; roadmap dedicated authoritative servers.
- **Risk:** Cross-platform input complexity.  
  **Mitigation:** action-map-driven controls + early device lab testing.

---

## 15) Definition of Done for Architecture C

- Clear module boundaries implemented via asmdefs/interfaces.
- 3-map progression data model defined and usable by designers.
- Host multiplayer operational with adapter seam for dedicated future.
- Live-ops hooks (telemetry + remote config) integrated but optional.
- Prototype-to-production path documented and executable.

