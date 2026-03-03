# Tank Puzzle Assault — Level Progression & Difficulty Curve

## 1) Design Goals

- **Puzzle-forward combat:** every arena requires bank shots, ramp usage, and smart destruction order, not only DPS racing.
- **Arc-shot mastery:** players learn to use parabolic shells to hit behind cover and over low walls.
- **Readable escalation:** enemy pressure rises predictably across 3 maps while introducing one new tactical problem at a time.
- **Rewarded experimentation:** Candy Crush-style node map encourages retries, branch exploration, and star-based unlock routing.

---

## 2) Candy Crush-Style Progression Map Structure

## World Flow
- Three themed worlds (Map 1 → Map 2 → Map 3), each shown as a curved node path.
- Each world has **5 playable nodes** (4 standard + 1 boss) for **15 total level concepts**.
- Players unlock the next node by earning at least **1 star** on the current node.
- Optional branch nodes award bonus stars, letting players skip one difficult mainline node per map.

## Star & Unlock Rules
- **1 star:** clear objective.
- **2 stars:** clear within target shot budget/time.
- **3 stars:** clear with no player KO (or no repair use).
- Gate between maps:
  - Unlock Map 2 at **10 total stars**.
  - Unlock Map 3 at **24 total stars**.

## Node Cadence Pattern (per map)
1. Tutorialized puzzle mechanic introduction
2. Combined mechanic pressure test
3. Enemy aggression spike
4. Resource management + positioning check
5. Boss showdown (mechanic mastery exam)

---

## 3) AI Enemy Pacing & Boss Placement Strategy

## Enemy Archetypes
- **Scout:** fast flanker, low HP, punishes over-aiming.
- **Bruiser:** slow heavy shelling, breaks destructibles quickly.
- **Mortar:** indirect arc fire; forces relocation.
- **Engineer:** rebuilds barricades / deploys cover drones.
- **Sniper:** long wind-up precision shot from high ground.
- **Rammer (elite):** closes distance via ramp lanes.

## Pacing Model (used in every level)
- **Phase A — Read (0–25%):** low enemy count, puzzle state is readable.
- **Phase B — Pressure (25–70%):** reinforcement pulses every 20–35 seconds or on objective milestone.
- **Phase C — Checkmate (70–100%):** elite pair or positional threat enters; forces final puzzle-combat execution.

## Boss Placement Strategy
- Boss is always final node of each map after a resource-check node to avoid unfair attrition.
- Each boss tests that map’s core mechanic:
  - Map 1 boss: line-of-sight break + destructible sequencing.
  - Map 2 boss: elevation control + crossfire denial.
  - Map 3 boss: multi-phase arena reconfiguration under pressure.
- Boss arenas include **one guaranteed health box at phase transition** (not at start) to reward survival skill.

---

## 4) Health, Mystery, and Power-Up Cadence

## Health Boxes
- Fixed spawns, visible on map preview.
- Cadence:
  - Map 1: every ~90 sec equivalent combat time.
  - Map 2: every ~110 sec.
  - Map 3: every ~130 sec.
- If player drops below 30% HP, next health spawn timer advances by 15 sec (assist without removing tension).

## Mystery Boxes
- 1 per non-boss level, 2 in late Map 3 levels.
- Risk/reward placement (exposed ramp crest, destructible trap pocket, or crossfire lane).
- Outcomes (weighted):
  - 40% useful (shield pulse, damage amp)
  - 35% situational (decoy drone, slow field)
  - 25% risky (short self-reveal, enemy aggro spike but grants score multiplier)

## Power-Up Cadence
- Guaranteed power-up on levels 1-2, 1-4, 2-3, 2-5, 3-2, 3-5.
- Random low chance on other nodes, capped to avoid streak RNG.
- Rotation priorities by map:
  - Map 1: **Arc Guide**, **Ricochet +1**
  - Map 2: **Ramp Boost**, **Armor Pierce**
  - Map 3: **EMP Shell**, **Overcharge Burst**

---

## 5) Map-by-Map Objectives & Level Concepts

## Map 1 — **Scrapyard Switchbacks** (Intro / Easy)
**Map Objective:** teach arc-shot fundamentals, destructible order, and safe ramp usage under light pressure.

| Node | Level Concept | Primary Objective | Enemy Mix & Pacing | Puzzle Focus | Pickups |
|---|---|---|---|---|---|
| 1-1 | Forklift Alley | Destroy 3 power relays behind low walls | Scouts only; gentle Phase B trickle | Arc over cover, basic bank shot | 1 health, 1 mystery, Arc Guide guaranteed |
| 1-2 | Rust Gate Relay | Open 2 gates by destroying marked crates in order | Scouts + 1 Bruiser late | Destructible sequencing introduces fail-state reset | 1 health, 1 mystery |
| 1-3 | Ramp Yard Cross | Capture center plate for 30s while under fire | Scouts + Bruiser pulse every 30s | First mandatory ramp lane usage | 1 health, Ricochet +1 chance |
| 1-4 | Compactor Maze | Escort payload through breakable maze | Scouts + Mortar intro in Phase C | Destroy/create lanes at correct timing | 1 health, 1 mystery, Ricochet +1 guaranteed |
| 1-5 (Boss) | Scrap Goliath | Defeat boss in 2 phases and disable shield pylons | Boss + Scout adds at 60% HP | Alternate between arc shots and pylon destruction windows | Health at phase swap, boss power-up drop |

## Map 2 — **Quarry Crossfire** (Mid / Medium)
**Map Objective:** force elevation control, deny enemy rebuilds, and manage mixed ranged pressure.

| Node | Level Concept | Primary Objective | Enemy Mix & Pacing | Puzzle Focus | Pickups |
|---|---|---|---|---|---|
| 2-1 | Split Ridge | Activate 3 ridge switches from opposite elevations | Scouts + Sniper intro | Multi-angle arc calculations from ramps | 1 health, 1 mystery |
| 2-2 | Blast Pit Lock | Collapse quarry pillars to block enemy reinforcements | Bruisers + Mortar | Timed destruction changes spawn topology | 1 health, Armor Pierce chance |
| 2-3 | Conveyor Ambush | Survive moving cover sequence and clear 4 signal towers | Scouts + Mortar + Engineer intro | Shoot while platforming/cover shifts | 1 health, 1 mystery, Ramp Boost guaranteed |
| 2-4 | Echo Canyon | Eliminate 2 snipers before opening core vault | Snipers + Bruiser escorts | Long-range threat prioritization with limited LOS | 1 health, 1 mystery |
| 2-5 (Boss) | Foreman Leviathan | Beat rotating weak-point boss and shut down rebuild drones | Boss + Engineer waves + Mortar support | Elevation swaps + anti-rebuild focus | Health at phase swap, Armor Pierce guaranteed |

## Map 3 — **Siege Foundry** (Late / Hard)
**Map Objective:** execute advanced multi-step puzzle plans while under relentless combined-arms AI pressure.

| Node | Level Concept | Primary Objective | Enemy Mix & Pacing | Puzzle Focus | Pickups |
|---|---|---|---|---|---|
| 3-1 | Heat Vent Gauntlet | Vent reactor pressure at 4 points in strict order | Scouts + Mortar + Sniper | Route planning under overlapping arcs | 1 health, 1 mystery |
| 3-2 | Shatterline Bridge | Destroy/restore bridge segments to bait elite pathing | Bruiser + Rammer intro + Engineer | Intentional terrain manipulation | 1 health, EMP Shell guaranteed |
| 3-3 | Furnace Carousel | Rotate central furnace for cover windows while capturing nodes | Mortar + Sniper + Engineers | Timing windows + moving LOS puzzle | 1 health, 2 mystery |
| 3-4 | Core Lockdown | Hold 2 distant control points with limited ammo refill | Full mixed roster, fast pulses (20s) | Resource discipline + high-ground denial | 1 health, Overcharge Burst chance |
| 3-5 (Final Boss) | Warden Prime | 3-phase finale: shield lattice, ramp duel, core exposure | Boss + elite mixed adds by phase | Full mechanic exam: arc precision + destruction order + ramp timing | Health at phase 2, Overcharge guaranteed |

---

## 6) Difficulty Curve Summary

- **Map 1 (Easy):** single-threat encounters, long telegraphs, plentiful recovery.
- **Map 2 (Medium):** dual-threat compositions, terrain-state puzzles, moderate attrition.
- **Map 3 (Hard):** layered indirect/direct fire, short decision windows, constrained recovery.
- Overall curve target: each new map raises average failure rate by ~8–12%, while optional branch nodes provide star recovery and learning loops.

This structure keeps progression readable and motivating while preserving the game’s puzzle-first tank identity.
