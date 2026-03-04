# Overnight Execution Checklist — 2026-03-04

**Last updated:** Wed 2026-03-04 01:28 PST  
**Owner:** PM (overnight manager log)  
**Repo state used:** `main` @ `8e43bc7`

## Goal by Morning
Have a Unity-openable **single-player puzzle campaign prototype** with playable L1-L3 blockouts, objective logic, and clear run instructions.

## Progress Snapshot (repo + PM updates)
- [x] Single-player campaign direction and L1–L12 blueprint docs are committed.
  - `planning/single-player-cutover-plan.md`
  - `docs/design/single-player-campaign-core.md`
  - `docs/design/single-player-level-blueprints.md`
- [x] Midnight Unity execution status captured: `planning/pm-updates/pm-midnight-status-2026-03-04.md`
- [x] PM midnight readiness update logged (this checklist refresh)
- [x] Morning handoff report drafted: `planning/pm-updates/morning-handoff-2026-03-04.md`
- [ ] L1–L3 scenes/prefabs mirrored into this repo's `Unity/Assets/Scenes` + `Unity/Assets/Prefabs`
- [ ] L1–L3 objective auto-wire logic committed and validated in repo
- [ ] HUD/menu/settings/keybind entry points committed in repo
- [ ] Unity smoke/compile validation evidence captured in repo for this overnight pass

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
- [x] Open project in Unity successfully *(validated in live Unity check; see PM midnight status)*
- [x] Open one of: `L1_FirstArc`, `L2_GateByDemolition`, `L3_CrossfireCapture` *(validated in live Unity check)*
- [ ] Press Play, player spawns + enemies spawn *(play-mode currently blocked by input-system exceptions)*
- [ ] Objective can complete and produce win-state event *(objective scripts reported but not fully scene-wired)*
- [ ] No compile/runtime-blocking errors in console *(runtime input exceptions currently present)*

## Notes
- Latest PM update confirms L1/L2/L3 scenes were generated in live Unity project `/Users/joe/dev/TankPuzzleAssult`, but are not yet mirrored into this docs repo.
- Input mismatch remains top blocker: project configured for new Input System while `DemoInputRouter` still calls legacy `UnityEngine.Input` API.

## Owner Model Policy
- Coding tasks: `litellm/worker` (Spark)
- PM/design/QA/docs: `openai-codex/gpt-5.3-codex`
