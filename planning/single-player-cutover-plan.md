# Single-Player Cutover Plan

**Project:** Tank Puzzle Assault  
**Date:** 2026-03-03  
**Owner:** PM  
**Source Direction:** GM update to prioritize single-player puzzle campaign first; leverage existing imported world assets; move multiplayer to later hardening.

## 1) Cutover Objective
Deliver a playable, coherent single-player campaign MVP loop first, using existing imported world assets and current prototype systems, then resume multiplayer hardening in a later phase.

---

## 2) Immediate Sprint Tasks (Single-Player Playable Loop)

## A. Campaign Flow + Progression
1. **Lock campaign MVP slice**
   - Define the minimum playable campaign path (recommended: one complete world arc with intro, escalation, and boss node).
   - Confirm mission objective taxonomy and fail/win conditions for each node.
2. **Implement world-map progression wiring**
   - Unlock sequencing, retry flow, checkpoint/restart behavior.
   - Results screen updates (completion, stars/score, next-node affordance).
3. **Save/load baseline for campaign state**
   - Persist unlocked nodes, best score/stars, selected class, and assist settings.

## B. Levelization Using Imported Assets (No Net-New Art by Default)
4. **Asset inventory and reuse pass**
   - Catalog imported world/environment pieces and tag by puzzle use (cover, destructible lane blocker, elevation/ramp support, objective enclosure).
5. **Build campaign nodes from existing assets**
   - Assemble each MVP node using imported assets first.
   - Record any missing-asset blockers explicitly (with severity and workaround).
6. **Puzzle readability polish pass**
   - Improve silhouette/readability, collision consistency, and telegraph clarity using existing assets/VFX/UI cues.

## C. Combat + AI Tuning for Solo Experience
7. **Tune core loop pacing for single-player**
   - Shot cadence, enemy spawn pressure, pickup timing, fail/retry friction.
8. **Boss encounter pass for campaign MVP**
   - Ensure boss phase transitions, weak-point clarity, and recovery windows are learnable solo.
9. **Assist + accessibility calibration**
   - Validate aim assist visibility/tuning, camera shake control, UI scale/contrast for puzzle comprehension.

## D. Stability + Ship-Readiness for MVP Gate
10. **Single-player regression sweep**
    - Complete scripted runthroughs for all MVP nodes on target platforms profile set (at minimum desktop/WebGL profile).
11. **Bug triage and closure for campaign blockers**
    - Close P0/P1 campaign blockers (progression break, objective lock, crash, soft-lock).
12. **Publish cutover verification report**
    - Summarize what is complete, known risks, and explicit handoff point to multiplayer hardening phase.

---

## 3) Deferred Multiplayer Items (Post-Campaign-MVP Hardening Phase)

1. Session/lobby UX completion (create/join/disconnect/rejoin resilience).
2. Authoritative sync hardening for destructibles/objectives under packet loss/jitter.
3. Co-op balancing pass (enemy pressure, pickup economy, revive/fail rules).
4. Multiplayer perf + bandwidth profiling across Web/Mobile/Xbox constraints.
5. Match-end summary/network error handling polish.
6. Expanded player-count testing beyond minimum co-op baseline.
7. Multiplayer-focused QA matrix and soak tests.

> Note: Multiplayer is not removed from product scope; it is intentionally resequenced after campaign MVP acceptance.

---

## 4) Campaign MVP Acceptance Criteria (Gate to Exit Priority Mode)

Campaign MVP is accepted when all criteria below are true:

1. **Playable campaign loop exists end-to-end in single-player**
   - Player can start from main menu, complete the defined MVP node path, and reach a final node/boss outcome.
2. **Progression is reliable**
   - Unlocks, retries, checkpoints, and post-level results behave consistently without progression loss.
3. **Imported-asset-first content target is met**
   - MVP nodes are primarily built from existing imported assets; any net-new assets are documented as justified blockers.
4. **Puzzle readability + difficulty ramp pass QA review**
   - No unresolved P0/P1 issues for objective clarity, unfair spikes, or unwinnable states.
5. **Single-player stability gate passes**
   - No crash/soft-lock in standard campaign playthrough; critical scripting/state bugs are closed.
6. **Cutover report published**
   - PM/Engineering publish a short report confirming campaign MVP acceptance and listing multiplayer hardening backlog for next phase.

---

## 5) Sequencing Rule (Effective Immediately)
All new implementation sequencing should prioritize single-player campaign completion and stability first. Multiplayer implementation continues only for blocking maintenance/compatibility needs until campaign MVP acceptance criteria are met.
