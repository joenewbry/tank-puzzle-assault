# Tank Puzzle Assault — Single-Player Campaign Core

## Purpose
Define the core design direction for a **single-player puzzle campaign** that reuses currently imported world assets and keeps the game focused on readable, satisfying arc-shot problem solving.

---

## 1) Core Campaign Fantasy

### One-line fantasy
> "I am a lone tank commander who solves battlefield riddles by bending terrain, timing arcs, and turning enemy fortifications against them."

### Player promise (single-player focus)
- Every encounter is a compact tactical puzzle, not a stat check.
- The player wins by reading terrain, not by twitch aim alone.
- Destruction is meaningful: every broken object changes routes, cover, or weak-point access.
- Retry is fast, so experimentation feels encouraged.

### Core emotional arc
1. **Observe**: the arena looks dangerous but understandable.
2. **Experiment**: one or two shots reveal the puzzle rule.
3. **Master**: the player chains arc + movement + destruction.
4. **Payoff**: the battlefield "clicks" and resolves cleanly.

---

## 2) Learning Curve for Arc-Shot Puzzles

Progression should teach one concept at a time, then combine it under pressure.

## Stage A — Arc Basics (safe sandbox)
- Teach tap vs hold shot bands with obvious over-wall targets.
- Failure should be informative (blocked shot, overshoot, undershoot).
- No enemy pressure during first two puzzle beats.

## Stage B — Destruction as Puzzle Language
- Introduce brittle blockers and explosive props.
- Teach "destroy this first" sequencing with immediate visual feedback.
- Keep encounter spaces compact so cause/effect remains clear.

## Stage C — Positioning and Lane Control
- Introduce ramps and offset lanes that require movement before firing.
- Add one flanking enemy type to force repositioning.
- Ensure player can still pause and read arena between threats.

## Stage D — Chained Interactions
- Multi-step puzzles: break support -> open angle -> arc to objective.
- Add indirect-fire threat (mortar) to pressure timing decisions.
- Keep at least one backup solve route to avoid hard-lock feel.

## Stage E — Mastery Exams
- Mixed enemy compositions plus destructible sequencing.
- Tight but fair windows for high-arc shots.
- Boss phases test previously learned puzzle verbs (not new surprise verbs).

### Difficulty tuning rules
- Introduce one new mechanic per level, combine on the following level.
- Any new mechanic appears in at least 2 low-pressure setups before high-pressure use.
- Checkpoint restart target: under 2 seconds.

---

## 3) Map Progression Overview (3 Maps)

## Map 1 — Scrapyard Switchbacks (Fundamentals)
**Learning goal:** understand arc bands, simple destructibles, and basic movement lanes.

- Typical puzzle verbs: over-cover arc, crate/barrel clears, first ramp alignment.
- Enemy pressure: mostly scouts + occasional bruiser.
- Completion test: player can identify the correct shot band in 1–2 attempts.

## Map 2 — Quarry Crossfire (Combination Play)
**Learning goal:** combine elevation, timing, and destructible sequencing under moderate pressure.

- Typical puzzle verbs: switchback ramp routing, pillar collapse, anti-rebuild priorities.
- Enemy pressure: mixed scouts/bruisers + mortar/sniper/engineer moments.
- Completion test: player can switch between solve planning and threat triage.

## Map 3 — Siege Foundry (Mastery)
**Learning goal:** execute multi-step plans while arena state changes rapidly.

- Typical puzzle verbs: route rewrite, chained collapses, moving cover timing.
- Enemy pressure: layered indirect + direct fire with elite interruptions.
- Completion test: player consistently uses environment manipulation as offense/defense.

### Campaign pacing recommendation
- 5 nodes per map (4 standard + 1 boss), 15 nodes total.
- Optional branch node per map for star recovery and low-stress practice.
- Boss node should always test prior mechanics, never introduce brand-new systems.

---

## 4) Level Readability & Accessibility Principles (Asset-Tied)

| Principle | Asset category (real source) | Implementation rule |
|---|---|---|
| **Silhouette first** | `AssetHunts!/GameDev Starter Kit - Tanks/Asset/Tank/*`, `PolygonalStudios/ToonTanksLowpoly/Prefabs/*` | Player, enemy, and boss tanks must be distinguishable by shape before color (important for colorblind readability). |
| **Destructible vs indestructible clarity** | `AssetHunts!/.../Asset/Prop/*`, `AssetHunts!/.../Asset/Building/*`, `AssetHunts!/.../Asset/Obstacle/*` | Use distinct material/edge language: breakables get chipped edges and hit VFX; permanent structures stay visually "solid" and unreactive. |
| **Navigation legibility** | `AssetHunts!/.../Asset/3D Tile/*`, `AssetHunts!/.../Asset/Ground/*`, `AssetHunts!/.../Asset/Rock/*` | Traversable surfaces need consistent value contrast and slope readability from gameplay camera. Avoid decorative clutter on critical lanes. |
| **Hazard telegraphing** | `AssetHunts!/.../Asset/Obstacle/Obstacle_Flamethrower_01.prefab`, mine/missile prefabs in `Asset/Weapon/*` | Any hazard should have a pre-attack tell (animation, glow, sound) and clear safe zone shape. |
| **Puzzle interaction affordance** | `AssetHunts!/.../Asset/Prop/Prop_Button_*`, `Prop_Lever_01.prefab`, bridge/wall props | Interactable objects need repeated visual grammar (icon, emissive accent, outline pulse on proximity). |
| **Objective guidance without UI overload** | `AssetHunts!/.../Asset/Collectible/*`, flags/sign props | Use environmental signposting (flags, lights, collectible placement) to pull player attention instead of constant on-screen text. |
| **Accessibility via material contrast** | `AssetHunts!/GameDev Starter Kit - Tanks/Material/*`, `PolygonalStudios/ToonTanksLowpoly/Materials/*` | Ensure objective/hazard colors pass contrast checks; add pattern or icon overlays so meaning is not color-only. |
| **Performance-safe readability** | Imported packs in `Assets/packs/*.unitypackage` | Keep readability-critical assets low complexity and reuse prefabs to maintain stable framerate on target platforms. |

---

## 5) Prefab/Asset Usage Checklist

Use this checklist while building campaign scenes. Replace placeholders with final prefab assignments.

### A) Core combat actors
- [ ] **Player tank prefab assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Tank/Player_Tank _GO-07 v01.prefab"` → `<PLAYER_TANK_PREFAB>`
- [ ] **Alt/player skin variant assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/PolygonalStudios/ToonTanksLowpoly/Prefabs/Tank1.prefab"` → `<PLAYER_ALT_VISUAL_PREFAB>`
- [ ] **Enemy basic tank assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Tank/Enemy_Tank_01.prefab"` → `<ENEMY_SCOUT_PREFAB>`
- [ ] **Enemy heavy/boss assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Tank/Enemy_Boss_Tank_01.prefab"` → `<ENEMY_BOSS_PREFAB>`

### B) Puzzle geometry and traversal
- [ ] **Base traversable tile set assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/3D Tile/3D_Tile_Ground_04.prefab"` → `<BASE_TILE_PREFAB>`
- [ ] **Ramp/slope tile assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/3D Tile/3D_Tile_Ground_Slope_02B.prefab"` → `<RAMP_TILE_PREFAB>`
- [ ] **Ground detail set assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Ground/Ground_Dune_A_01.prefab"` → `<GROUND_DETAIL_PREFAB>`

### C) Destructibles, hazards, and interaction language
- [ ] **Primary breakable cover assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Prop/Prop_Crate_01.prefab"` → `<BREAKABLE_COVER_PREFAB>`
- [ ] **Explosive chain object assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Prop/Prop_Wooden_Barrel_01.prefab"` → `<EXPLOSIVE_PROP_PREFAB>`
- [ ] **Support-collapse piece assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Prop/Prop_Wooden_Bridge_01_A.prefab"` → `<SUPPORT_COLLAPSE_PREFAB>`
- [ ] **Hazard obstacle assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Obstacle/Obstacle_Flamethrower_01.prefab"` → `<HAZARD_PREFAB>`
- [ ] **Interaction trigger prop assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Prop/Prop_Button_01.prefab"` → `<INTERACT_BUTTON_PREFAB>`

### D) Objective and reward signposting
- [ ] **Objective marker collectible assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Collectible/Collectible_Star_02.prefab"` → `<OBJECTIVE_MARKER_PREFAB>`
- [ ] **Secondary reward collectible assigned**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/AssetHunts!/GameDev Starter Kit - Tanks/Asset/Collectible/Collectible_Gear_01.prefab"` → `<BONUS_REWARD_PREFAB>`

### E) Provenance / package tracking
- [ ] **Asset source package tracked (AssetHunts)**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/packs/assethunts_gamedev_starter_kit_tanks_v100.unitypackage"` → `<SOURCE_PACKAGE_ASSETHUNTS>`
- [ ] **Asset source package tracked (PolygonalStudios)**  
  `"/Users/joe/dev/TankPuzzleAssult/Assets/packs/toontankslowpoly.unitypackage"` → `<SOURCE_PACKAGE_POLYGONAL>`

---

## 6) Implementation Notes (Single-Player Scope Guardrails)
- Keep campaign logic authored for solo play first; do not require co-op actions for puzzle completion.
- Prefer deterministic puzzle states and explicit reset points to support quick retry loops.
- During graybox, validate every level with this question: **"Can a new player read the intended first action in under 5 seconds?"**

**Status:** Ready for campaign pre-production and level blockout.