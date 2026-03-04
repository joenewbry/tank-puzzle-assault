# Tank Puzzle Assault — Single-Player Level Blueprints (L1–L12)

## Scope
This document defines a 12-level single-player progression focused on puzzle-combat encounters using fixed-arc tank shots, destructibles, and ramps.

All blueprints reference imported asset families from:
- `Assets/AssetHunts!/GameDev Starter Kit - Tanks`
- `Assets/PolygonalStudios/ToonTanksLowpoly`
- `Assets/packs` (source package references)

---

## Environment Asset Set Library (for level recommendations)

### Set A — Desert Outpost Intro
- **Primary paths:**
  - `AssetHunts!/GameDev Starter Kit - Tanks/Asset/3D Tile` (`3D_Tile_Flat_Ground_Desert_01`, `3D_Tile_Ground_Slope_01/02`)
  - `.../Asset/Building` (`Building_Desert_House_A_01..03`)
  - `.../Asset/Plant` (`Plant_Cactus_01/02`, `Plant_Dead_Tree_01`)
  - `.../Asset/Prop` (`Prop_Crate_01..04`, `Prop_Wooden_Barrel_01`, `Prop_Sandbag_01`)

### Set B — Scrapyard/Depot
- **Primary paths:**
  - `.../Asset/3D Tile` (`3D_Tile_Flat_Road_01..03`, `3D_Tile_Flat_Ground_Desert_to_Road`)
  - `.../Asset/Prop` (`Prop_Container_01/02`, `Prop_Oil_Storage_01`, `Prop_Oil_Storage_Tank_01`, `Prop_Railway_Track_01`)
  - `.../Asset/Obstacle` (`Obstacle_Barricade_01`, `Obstacle_Czech_Hedgehog_01`)
  - `.../Asset/Vehicle` (`Vehicle_Destroyed_Tank_01`, `Vehicle_Train_A_01..03`)

### Set C — Quarry Ridge
- **Primary paths:**
  - `.../Asset/Rock` (`Rock_A_01..06`, `Rock_B_01..03`)
  - `.../Asset/3D Tile` (`3D_Tile_Ground_02/03`, `3D_Tile_Ground_Slope_03/04`)
  - `.../Asset/Prop` (`Prop_Army_Watch_Tower_01`, `Prop_Construction_Barricade_01`, `Prop_Modular_Pipe_A_01`)

### Set D — Frozen Checkpoint
- **Primary paths:**
  - `.../Asset/3D Tile` (`3D_Tile_Flat_Ice_01`, `3D_Tile_Ice_01`)
  - `.../Asset/Plant` (`Plant_Tree_Ice_01`, `Plant_Tree_Ice_01A`)
  - `.../Asset/Prop` (`Prop_Snowman_01`, `Prop_Fence_B_01`)

### Set E — Foundry/Lava Siege
- **Primary paths:**
  - `.../Asset/3D Tile` (`3D_Tile_Flat_Lava_01`, `3D_Tile_Lava_01`)
  - `.../Asset/Prop` (`Prop_Modular_Pipe_A_01/02`, `Prop_Pumpjack_01`, `Prop_Oil_Storage_01`)
  - `.../Asset/Obstacle` (`Obstacle_Dragons_Teeth_01`, `Obstacle_Pusher_01`)

### Tank Visual Set (all levels)
- `PolygonalStudios/ToonTanksLowpoly/Prefabs/Tank1..Tank13` for player/enemy visual variety.

---

## Level Blueprints

## L1 — First Arc
- **Objective:** Destroy 3 relay targets behind low cover, then eliminate 2 scouts.
- **Key obstacle / destructible / ramp pattern:** Two low crate walls force first lob shot; one short ramp introduces elevation shot correction.
- **Enemy composition:** 2x Scout (staggered, one at 45s).
- **Powerup placements:**
  - Health box at safe back-left lane (after second relay)
  - SpeedBoost in optional right flank pocket (risk lane)
- **Recommended environment asset set:** **Set A (Desert Outpost Intro)**.

## L2 — Gate by Demolition
- **Objective:** Open two gates by destroying marked support crates in sequence A→B.
- **Key obstacle / destructible / ramp pattern:** Central reinforced lane blocked until side destructibles are removed; dual mirrored ramps create alternate firing arcs.
- **Enemy composition:** 2x Scout + 1x Bruiser (spawns once first gate opens).
- **Powerup placements:**
  - Shield at midpoint between ramp exits
  - Health behind destructible side hut (reward for puzzle solve)
- **Recommended environment asset set:** **Set A + Set B hybrid**.

## L3 — Crossfire Capture
- **Objective:** Hold center capture plate for 30s while maintaining at least one open retreat lane.
- **Key obstacle / destructible / ramp pattern:** Four quadrant barricades; player can pre-break two walls to shape sightlines; one long ramp controls center dominance.
- **Enemy composition:** 3x Scout (waves) + 1x Mortar (late pressure).
- **Powerup placements:**
  - MultiShot on exposed ramp crest (high risk)
  - Health in rear trench opposite mortar nest
- **Recommended environment asset set:** **Set B (Scrapyard/Depot)**.

## L4 — Split Switchyard
- **Objective:** Activate 3 switches on opposite sides, then destroy command node.
- **Key obstacle / destructible / ramp pattern:** Rail-track lanes with destructible cargo containers; slope pair enables bank/lob route to protected switch.
- **Enemy composition:** 2x Scout + 1x Bruiser + 1x Engineer.
- **Powerup placements:**
  - SpeedBoost near rail crossover
  - Shield behind optional destroyable train car cover
- **Recommended environment asset set:** **Set B (Scrapyard/Depot)**.

## L5 — Ridgeline Angle Test
- **Objective:** Destroy 4 ridge emplacements in under 3 minutes.
- **Key obstacle / destructible / ramp pattern:** Alternating high/low rock ridges with mandatory long-lob lanes; destructible bridge plank creates shortcut.
- **Enemy composition:** 2x Scout + 2x Sniper.
- **Powerup placements:**
  - Health in central low ravine (contested)
  - Armor-pierce style pickup equivalent (MultiShot proxy) on high right ridge
- **Recommended environment asset set:** **Set C (Quarry Ridge)**.

## L6 — Pillar Collapse Run
- **Objective:** Collapse 3 structural pillars to stop reinforcement conveyor.
- **Key obstacle / destructible / ramp pattern:** Pillars behind partial cover; each collapse changes pathing and opens/blocks lanes; two short ramps for angle correction.
- **Enemy composition:** 1x Bruiser + 2x Mortar + 1x Scout reinforcements.
- **Powerup placements:**
  - Shield near pillar #2 (center)
  - Health only unlocked after pillar #3 collapse
- **Recommended environment asset set:** **Set C (Quarry Ridge)**.

## L7 — Ice Relay Ambush
- **Objective:** Route power through 3 frozen relays while avoiding sniper lock lanes.
- **Key obstacle / destructible / ramp pattern:** Slippery ice flats (wide open) with breakable fence islands; one long snow ramp for hard-angle counterplay.
- **Enemy composition:** 2x Sniper + 2x Scout + 1x Engineer.
- **Powerup placements:**
  - SpeedBoost near frozen bridge entry
  - Health in central ice bowl (visible bait)
- **Recommended environment asset set:** **Set D (Frozen Checkpoint)**.

## L8 — Quarry Boss Antechamber
- **Objective:** Survive 2 minutes, then destroy 2 shield pylons simultaneously (within 20s).
- **Key obstacle / destructible / ramp pattern:** Twin elevated pylons protected by destructible barricade rings; mirrored ramps create synchronized shot routes.
- **Enemy composition:** 1x Bruiser + 1x Mortar + 2x Engineer + Scout trickle.
- **Powerup placements:**
  - Shield before final 30s wave
  - MultiShot between pylon lanes for synchronized takedown
- **Recommended environment asset set:** **Set C + Set B hybrid**.

## L9 — Foundry Vent Circuit
- **Objective:** Vent 4 pressure valves in strict order under constant bombardment.
- **Key obstacle / destructible / ramp pattern:** Lava channels divide arena; destructible pipe covers hide valve weak points; steep ramps connect safe islands.
- **Enemy composition:** 2x Mortar + 2x Scout + 1x Sniper.
- **Powerup placements:**
  - Health after valve #2 in temporary safe pocket
  - Shield before crossing final lava bridge
- **Recommended environment asset set:** **Set E (Foundry/Lava Siege)**.

## L10 — Smelter Carousel
- **Objective:** Capture rotating control nodes (A-B-C) while disabling two enemy generators.
- **Key obstacle / destructible / ramp pattern:** Moving cover rhythm simulated by timed obstacle pushers; destructible pipe bundles open temporary firing windows.
- **Enemy composition:** 1x Bruiser + 2x Engineer + 2x Scout + 1x Mortar late.
- **Powerup placements:**
  - SpeedBoost in outer ring to support rotation pace
  - Health under central gantry (dangerous but high value)
- **Recommended environment asset set:** **Set E (Foundry/Lava Siege)**.

## L11 — Lockdown Gauntlet
- **Objective:** Hold two remote uplink pads (20s each), then defeat elite pair.
- **Key obstacle / destructible / ramp pattern:** Dragons-teeth choke points and breakable barricade gates force route planning; dual long ramps enable emergency rotates.
- **Enemy composition:** 2x Sniper + 1x Mortar + 2x Engineer + 1x Rammer elite.
- **Powerup placements:**
  - Shield on first uplink pad completion
  - MultiShot before elite pair phase
  - Health placed off-main path as comeback option
- **Recommended environment asset set:** **Set E + Set C hybrid**.

## L12 — Final Exam: Bastion Core
- **Objective:** Three-phase encounter: destroy side supports, survive arena rewrite, burst exposed core.
- **Key obstacle / destructible / ramp pattern:** Phase 1 uses support towers; phase 2 spawns temporary rubble walls; phase 3 opens dual-ramp core lanes with destructible final armor shell.
- **Enemy composition:** Boss tank + mixed adds by phase (Scout → Mortar/Engineer → Sniper/Rammer).
- **Powerup placements:**
  - Phase transition health box (between P1/P2)
  - Shield at P2 midpoint
  - MultiShot only during final core exposure window
- **Recommended environment asset set:** **Set E (Foundry/Lava Siege) + ToonTanksLowpoly boss variant**.

---

## First Playable Subset (L1–L3) — Exact Blockout Instructions

> Use these for immediate blockout scenes in Unity. Values below are deterministic placements (not procedural).

## Global Blockout Conventions
- **Grid unit:** `1 cell = 4m`
- **Floor tile default:** `3D_Tile_Flat_Ground_Desert_01` (or road variant where noted)
- **Origin:** `(0,0,0)` at arena center; coordinates listed as `(X,Z)` cell coordinates
- **Y levels:** Base floor `Y=0`, raised lane `Y=2`, tall ridge `Y=4`
- **Player spawn prefab marker:** `Prop_Tank_Spawn_01`
- **Enemy visual prefabs:** `PolygonalStudios/ToonTanksLowpoly/Prefabs/Tank1..Tank13`
- **Destructible setup:** Add `DestructibleObject` to crates/barrels/support props; default HP 50, support HP 80.
- **Powerups:** use `PowerupBox` with types from implementation (`Health`, `Shield`, `SpeedBoost`, `MultiShot`).

### L1 Blockout — "First Arc" (24x16 cells)

#### 1) Arena footprint
1. Create a `24 x 16` floor from `3D_Tile_Flat_Ground_Desert_01` covering cell ranges `X:-12..11`, `Z:-8..7`.
2. Border walls: place `Prop_Broken_Wall_A_01` continuously on perimeter except a 2-cell opening at south edge center (`X:-1..0, Z:-8`) for player entry.

#### 2) Puzzle objects and ramps
1. Relay targets (simple placeholder destructibles):
   - Relay A at `(-4, 3)` behind crate wall
   - Relay B at `(2, 4)` behind crate wall
   - Relay C at `(7, 1)` behind low sandbag line
2. Crate walls (`Prop_Crate_03`) at:
   - Row 1: `(-5,2), (-4,2), (-3,2)`
   - Row 2: `(1,3), (2,3), (3,3)`
3. Place one ramp `3D_Tile_Ground_Slope_01` at `(5,-1)` facing north to create high-arc lane to Relay C.

#### 3) Spawns and enemy setup
1. Player spawn marker `Prop_Tank_Spawn_01` at `(0,-6)`.
2. Enemy Scout spawn points:
   - E1 at `(-8,6)` active at start
   - E2 at `(9,6)` delayed spawn at 45s or after Relay B destroyed.

#### 4) Powerups
1. Health (`PowerupBox: Health`) at `(-10,-2)`.
2. SpeedBoost at `(10,-1)` (optional flank route).

#### 5) Win conditions
1. Destroy Relays A/B/C.
2. Eliminate both scouts.
3. Fail/Reset trigger if player is destroyed; restart at `(0,-6)`.

---

### L2 Blockout — "Gate by Demolition" (28x18 cells)

#### 1) Arena footprint
1. Base floor `28 x 18` using `3D_Tile_Flat_Ground_Desert_to_Road` in center strip and desert tile elsewhere (`X:-14..13`, `Z:-9..8`).
2. Add two closed gate lanes at north side using `Obstacle_Barricade_01`:
   - Gate A centered at `(-5,7)` width 3 cells
   - Gate B centered at `(6,7)` width 3 cells

#### 2) Destruction sequence layout
1. Support crate cluster A (must be destroyed first) at `(-9,1), (-8,1), (-9,2)`.
2. Support crate cluster B at `(8,1), (9,1), (9,2)`.
3. Script trigger order: Cluster A opens left half of central lane; Cluster B opens final path to objective node at `(0,7)`.
4. Place mirrored ramps:
   - Left ramp `3D_Tile_Ground_Slope_02` at `(-6,-2)` facing north-east
   - Right ramp `3D_Tile_Ground_Slope_02A` at `(6,-2)` facing north-west

#### 3) Enemy composition placement
1. Scouts:
   - S1 at `(-11,6)` active at start
   - S2 at `(11,6)` active at start
2. Bruiser:
   - B1 at `(0,8)` spawns when Gate A opens.

#### 4) Powerups
1. Shield at `(0,-1)` between ramp exits.
2. Health hidden behind hut (`Building_Desert_House_A_02`) at `(-12,4)`.

#### 5) Win condition
1. Destroy cluster A then cluster B.
2. Reach and destroy objective node at `(0,7)`.
3. Eliminate remaining enemies.

---

### L3 Blockout — "Crossfire Capture" (30x20 cells)

#### 1) Arena footprint
1. Use road/scrapyard mix floor (`3D_Tile_Flat_Road_01` lanes, desert filler) for `X:-15..14`, `Z:-10..9`.
2. Center capture plate marker at `(0,0)` (radius 2 cells).
3. Create four quadrant barricades (`Obstacle_Barricade_01`) centered at:
   - NW `(-5,4)`, NE `(5,4)`, SW `(-5,-4)`, SE `(5,-4)`

#### 2) Destructible and ramp pattern
1. Place destructible wall chunks (`Prop_Broken_Wall_A_02`) at `(-2,3), (2,3), (-2,-3), (2,-3)` to let players open two chosen sightlines.
2. Long center-right ramp `3D_Tile_Ground_Slope_03` at `(8,0)` facing west toward capture plate.
3. Mortar nest platform (raised Y=2) tiles at `(10,6), (11,6), (10,7), (11,7)`.

#### 3) Enemy composition and timing
1. Scout wave 1: `(-12,8)` at t=0.
2. Scout wave 2: `(12,8)` at t=20s.
3. Scout wave 3: `(0,9)` at t=40s.
4. Mortar: `(10,7)` at t=50s (on raised platform).

#### 4) Powerups
1. MultiShot at `(8,2)` on exposed ramp approach.
2. Health at `(-11,-7)` behind `Prop_Container_01` cover.

#### 5) Objective logic
1. Player must hold center plate `(0,0)` for cumulative 30s.
2. Capture pauses while no player tank is inside radius.
3. Mission complete after timer reaches 30s and all active enemies are destroyed.

---

## Implementation Notes for Designers
- Keep all puzzle-relevant destructibles visually distinct (color tint/material swap).
- For first playable, prioritize readability over decoration density.
- Build L1–L3 as separate scenes (`SP_L1_FirstArc`, `SP_L2_GateByDemolition`, `SP_L3_CrossfireCapture`) under `Unity/Assets/Scenes/`.
- Reuse this blueprint structure for L4–L12 production passes.
