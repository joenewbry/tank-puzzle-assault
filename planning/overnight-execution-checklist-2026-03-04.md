# Overnight Execution Checklist — 2026-03-04

**Last updated:** Wed 2026-03-04 01:21 PST  
**Owner:** PM (overnight manager log)  
**Repo state used:** `main` @ `3ee65a6`

## Goal by Morning
Have a Unity-openable **single-player puzzle campaign prototype** with playable L1-L3 blockouts, objective logic, and clear run instructions.

## Progress Snapshot (repo + reporting artifacts)
- [x] Single-player campaign direction and L1–L12 blueprint docs are committed.
  - `planning/single-player-cutover-plan.md`
  - `docs/design/single-player-campaign-core.md`
  - `docs/design/single-player-level-blueprints.md`
- [x] PM midnight readiness update logged (this checklist refresh).
- [x] Morning handoff report drafted: `planning/pm-updates/morning-handoff-2026-03-04.md`
- [ ] L1–L3 scenes committed in `Unity/Assets/Scenes/`
- [ ] L1–L3 objective auto-wire logic committed and validated
- [ ] HUD/menu/settings/keybind entry points committed
- [ ] Unity smoke/compile validation evidence captured for this overnight pass

## In Flight
- [ ] Objective systems pass (L1-L3 win conditions + auto-wire)
- [x] PM midnight readiness update

## Next Queue (Auto-execute)
1. [ ] Integrate real tank prefabs/visual polish for L1-L3 (no placeholder cubes for player/enemies)
2. [ ] Enemy behavior tuning pass for puzzle pacing (L1-L3)
3. [ ] UI/HUD pass (objective status + win/fail text)
4. [ ] Settings/menu sanity pass (audio + keybind entry points)
5. [ ] Unity batch smoke pass + compile validation
6. [x] Morning handoff report (what to open, what scene to run, known gaps)

## Definition of “Ready to Play” (Morning)
- [ ] Open project in Unity successfully
- [ ] Open one of: `L1_FirstArc`, `L2_GateByDemolition`, `L3_CrossfireCapture`
- [ ] Press Play, player spawns + enemies spawn
- [ ] Objective can complete and produce win-state event
- [ ] No compile errors in console

## Notes
- This checklist is updated from current committed repo state and PM reporting artifacts only.
- No new overnight engineering scene/prefab commits were present at time of update.

## Owner Model Policy
- Coding tasks: `litellm/worker` (Spark)
- PM/design/QA/docs: `openai-codex/gpt-5.3-codex`
