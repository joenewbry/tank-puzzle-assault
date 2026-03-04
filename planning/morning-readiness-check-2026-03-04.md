# Morning Readiness Check — 2026-03-04

**Timestamp:** Wed 2026-03-04 01:21 PST  
**QA owner:** overnight pass (subagent)  
**Docs repo:** `/Users/joe/dev/openarcade/tank-puzzle-assault`  
**Unity project checked:** `/Users/joe/dev/TankPuzzleAssult`  
**Unity editor target:** `6000.3.10f1`

## What was validated overnight

- Unity batch open/compile smoke run:
  - `Unity -batchmode -nographics -quit -projectPath /Users/joe/dev/TankPuzzleAssult -logFile /tmp/tpa-unity-open-smoke.log`
- Scene presence/build list check for:
  - `Assets/Scenes/L1_FirstArc.unity`
  - `Assets/Scenes/L2_GateByDemolition.unity`
  - `Assets/Scenes/L3_CrossfireCapture.unity`
- Script/prefab wiring spot checks for spawn/combat/objective readiness.

---

## Explicit pass/fail checklist (open + play L1–L3)

### Global gate (must pass before any level play validation)

- [ ] **FAIL** — Project opens with clean compile (0 script errors)
  - Blocker: `Assets/Scripts/UI/ObjectiveHUDController.cs(28,13): error CS0103: The name 'LevelManager' does not exist in the current context`

### Level-by-level readiness

| Check | L1_FirstArc | L2_GateByDemolition | L3_CrossfireCapture |
|---|---|---|---|
| Scene asset exists in project | PASS | PASS | PASS |
| Scene listed in Build Settings | PASS | PASS | PASS |
| Can enter Play Mode and validate gameplay loop | **FAIL** (blocked by compile error above) | **FAIL** (blocked by compile error above) | **FAIL** (blocked by compile error above) |
| Spawn manager + GameLoopManager present in scene | PASS | PASS | PASS |
| Objective completion/win-state path verifiable | **FAIL** (objective scripts not wired in scene) | **FAIL** (objective scripts not wired in scene) | **FAIL** (objective scripts not wired in scene) |

**Overall morning readiness:** **NOT READY** (compile blocker + gameplay wiring gaps)

---

## Known issues (with severity)

1. **CRITICAL** — Compile blocker prevents Play Mode
   - File: `Assets/Scripts/UI/ObjectiveHUDController.cs:28`
   - Issue: references `LevelManager.Instance?.CompleteLevel()` but no `LevelManager` exists in project scripts.

2. **HIGH** — L1/L2/L3 objective flow not wired in scenes
   - No `ObjectiveTracker`, `ObjectiveRelayTarget`, or `TimedCaptureZone` components detected in L1–L3 scene assets.
   - Result: no explicit objective-complete/win trigger path in current scene wiring.

3. **HIGH** — Spawned enemies are not gameplay-active by default prefab wiring
   - `Assets/Prefabs/EnemyPrefab.prefab` contains mesh/collider/rigidbody but no `EnemyTankAI` / no `ProjectileDamageReceiver`.
   - Result: enemies can spawn but are likely non-behavioral/non-damageable in current setup.

4. **MEDIUM** — Player prefab is movement-only for now
   - `Assets/Prefabs/PlayerPrefab.prefab` currently has `DemoInputRouter` but no `ProjectileDamageReceiver`.
   - Result: combat/health-powerup interactions are incomplete.

5. **MEDIUM** — Physics architecture inconsistency still present
   - `EnemyTankAI` uses `Rigidbody2D`; player/prefabs use 3D `Rigidbody`/`Collider`.
   - Result: higher risk of interaction and tuning issues when gameplay scripts are fully wired.

---

## Exactly what Joe should click/open in Unity in the morning

1. Open **Unity Hub**.
2. In Projects, click **Open** (or Add) and select:
   - `/Users/joe/dev/TankPuzzleAssult`
3. Ensure editor version is **6000.3.10f1** and open project.
4. In Unity, open **Window → General → Console**.
5. Click **Clear** in Console.
6. In Project panel, open **Assets → Scenes**.
7. Double-click in this order:
   1. `L1_FirstArc.unity`
   2. `L2_GateByDemolition.unity`
   3. `L3_CrossfireCapture.unity`
8. After scripts compile, if Console shows red errors, **double-click the first error**:
   - expected current blocker: `ObjectiveHUDController.cs` line 28 (`LevelManager` missing)
9. Once compile is clean, press **Play** (top-center ▶) on each scene and verify:
   - player spawns,
   - enemies spawn and move/engage,
   - objective can complete and a win/complete signal appears.
10. If blocker persists, first triage target is:
   - `Assets/Scripts/UI/ObjectiveHUDController.cs` (remove/replace `LevelManager` dependency).
