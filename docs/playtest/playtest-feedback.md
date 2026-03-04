# TPA-017 Playtest-Style Feedback (Code/Design Review)

## Scope & Method
This is a **playtest-style assessment based on design/docs + code review** (not a full hands-on Unity run).
Reviewed:
- Design intent: `docs/design/*.md`, `docs/PRD.md`
- Current prototype scripts: `Unity/Assets/Scripts/**/*`
- Progression data: `Unity/Assets/Resources/Data/MapProgressionData.json`

---

## 1) Likely Player Experience in the First 10 Minutes

### What will likely feel good
- **Immediate fantasy is strong**: tank movement + destruction + arc shots is a compelling core loop on paper.
- **Low control surface** (move + rotate + charge/fire) supports fast onboarding.
- **Environment-driven problem solving** (destructibles, ramps, cover) should create early “aha” moments.

### What likely happens minute-by-minute (current prototype trajectory)
- **0:00–2:00**: Player quickly understands movement and firing cadence.
- **2:00–5:00**: Arc-shot understanding is likely inconsistent because charge bands/landing feedback are not yet surfaced in gameplay code.
- **5:00–8:00**: Enemy pressure appears but readability is mixed; AI behavior currently feels more “state-machine basic” than role-distinct tactical archetypes.
- **8:00–10:00**: Player sees potential depth, but clarity gaps (shot prediction, enemy telegraphing, objective readability) may suppress confidence.

### Net first-impression summary
- **Concept clarity**: high
- **Moment-to-moment readability**: medium-low in current implementation
- **Retention potential after first session**: good if feedback/telegraphing pass is prioritized before broader content expansion

---

## 2) Frustration Points / Clarity Issues

1. **Arc predictability gap (high impact)**
   - Current arc math and aiming UX do not yet fully support the “read then act” promise.
   - `ProjectileArcSolver` (Gameplay) currently computes launch vectors without clearly aligning horizontal direction to target vector; this risks unintuitive misses.
   - No implemented landing marker/ghost-arc teaching loop yet.

2. **Aiming camera/world conversion ambiguity**
   - `TankControllerBase` uses `ScreenToWorldPoint(Input.mousePosition)` without explicit depth-plane handling, which can produce confusing turret orientation depending on camera setup.

3. **2D/3D systems mismatch**
   - Player movement uses `Rigidbody` (3D) while enemy AI uses `Rigidbody2D`, increasing integration risk and perceived inconsistency in feel/collisions.

4. **Enemy readability currently below design target**
   - AI variants are intended as role-readable (Scout/Bruiser/Mortar/etc.), but behavior implementation is still generalized.
   - Patrol logic currently picks random direction frequently, likely causing jittery/non-intentional movement reads.

5. **Powerups are present but under-communicated**
   - Pickup effects are stubs in `PowerupBox`; without strong VFX/UI messaging, pickups may feel random or low-impact.

6. **Prototype-to-design alignment risk**
   - Design docs specify named world progression and puzzle-teaching cadence; JSON progression currently follows a separate naming/content structure.
   - This can create pacing inconsistency in early playtests.

7. **Pre-playtest technical blocker to resolve first**
   - Duplicate `DestructibleObject` class names in `Scripts/Core` and `Scripts/Gameplay` likely cause compile conflict unless namespaced/removed.

---

## 3) Fun / Replayability Opportunities

1. **“Two-solve” room design (Blue precision vs Green demolition)**
   - Strongest replay driver: same room solved through different tank identities.

2. **Style scoring + puzzle efficiency together**
   - Existing scoring design can create a great “clear once, optimize forever” loop if lane breakdown is visible after each stage.

3. **Chain-reaction sandbox moments**
   - Destructible supports + explosive objects + ricochet can generate clip-worthy emergent moments.

4. **Short daily seed challenges**
   - 5–8 minute deterministic challenge seeds fit web/mobile retention nicely.

5. **Ghost replays for learning**
   - Seeing successful arc lines from prior runs/friends directly teaches trajectory literacy and reduces frustration.

---

## 4) Suggested Tuning Changes

## A) Arc shots (highest priority)
1. **Lock charge bands to explicit timing + UI labels**
   - Quick: `0.00–0.25s`
   - Standard: `0.25–0.80s`
   - Lob: `0.80–1.50s`
2. **Set conservative launch-speed spreads early**
   - Blue: `22 / 28 / 34`
   - Green: `19 / 25 / 31`
   (Keeps trajectories readable in first map.)
3. **Implement landing marker confidence colors now**
   - White = terrain, Yellow = destructible in radius, Red = enemy in radius.
4. **Teach via miss feedback**
   - 0.5s ghost arc on miss in Map 1 only (optional toggle after onboarding).
5. **Unify physics stack**
   - Pick 2D or 3D for tanks/projectiles/AI and keep all combat actors in one physics domain.

## B) AI pacing & combat readability
1. **Concurrency caps by map**
   - Map 1: max 3 active threats
   - Map 2: max 5
   - Map 3: max 7
2. **Pulse cadence**
   - Reinforcements every ~35s (Map 1), ~28s (Map 2), ~20–22s (Map 3).
3. **Telegraph floor**
   - Mortar wind-up >= 0.9s; sniper wind-up >= 1.2s with clear line indicator.
4. **Role-lock first 10 minutes**
   - Do not mix more than two enemy roles in onboarding slice.
5. **Boss scaling safety**
   - Use absolute per-phase stat targets instead of multiplicative stacking to avoid runaway speed/radius values.

## C) Powerups
1. **Guaranteed early utility cadence**
   - Guarantee one defensive/clarity tool in first 2 nodes (e.g., Arc Guide, light shield).
2. **Readable rarity buckets**
   - Helpful 55% / situational 30% / risky 15% for onboarding map.
3. **Immediate feedback requirement**
   - Every pickup needs: icon popup, short SFX, and visible temporary buff indicator.
4. **Avoid “dead” pickups**
   - Keep all early-game powerups impactful within 10 seconds of pickup.

## D) Progression map tuning
1. **Align implementation data with design arc**
   - Ensure map/node structure in JSON mirrors the documented 1-1..3-5 world progression intent.
2. **Star gate softness for first pass**
   - Consider Map 2 unlock at 8 stars (instead of 10) for first public test cohort; review telemetry before locking.
3. **One optional branch per world minimum**
   - Supports skill recovery and reduces churn at difficulty spikes.
4. **Difficulty ramp check**
   - Target fail-rate increase per map around +8–12% (as documented) and verify with telemetry before content scale-up.

---

## Recommended Immediate Next Actions (Before External Playtests)
1. Resolve compile/integration blockers (duplicate class naming + physics domain consistency).
2. Implement arc landing marker + miss ghost arc.
3. Stabilize enemy role readability with explicit telegraphs and capped concurrency.
4. Align progression JSON with intended map/node teaching cadence.
5. Then run 5-player internal observed playtest focused only on first 10 minutes.

Overall: **the design foundation is strong and replay-capable; clarity/feedback tuning is the critical path to converting that potential into first-session fun.**
