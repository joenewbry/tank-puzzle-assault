# Unity Open + Play Smoke (L1–L3)

**Project path:** `/Users/joe/dev/TankPuzzleAssult`  
**Editor:** `6000.3.10f1`

## 1) Open + compile gate

1. Open Unity Hub → open project at path above.
2. Open **Window → General → Console** and clear logs.
3. Wait for compile/import to finish.
4. **Pass if:** 0 red compile errors.
5. **Fail if:** any script error (current known blocker: `ObjectiveHUDController.cs` missing `LevelManager`).

## 2) Scene smoke loop (run for each scene)

Scenes:
- `Assets/Scenes/L1_FirstArc.unity`
- `Assets/Scenes/L2_GateByDemolition.unity`
- `Assets/Scenes/L3_CrossfireCapture.unity`

For each scene:
1. Double-click scene to open.
2. Press **Play**.
3. Verify in first 20–30s:
   - Player spawns and can move (WASD/arrow keys).
   - Enemies spawn.
   - No new red Console errors/exceptions.
4. Try to reach objective completion condition.
5. Stop Play.

## 3) Pass criteria

- Compile gate passes.
- All 3 scenes enter Play Mode.
- All 3 scenes spawn player + enemies without runtime exceptions.
- Objective/win-state can be completed in each scene.

## 4) Fail handling

- Capture first error line + file + line number from Console.
- Log failing scene name and step.
- Block release/readiness if compile gate fails.
