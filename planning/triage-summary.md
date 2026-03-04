# TPA-021 PM Triage Summary — v1 Scope Lock

- **Project:** Tank Puzzle Assault
- **Task:** TPA-021 (PM Triage)
- **Date:** 2026-03-03
- **Inputs Reviewed:**
  - `docs/architecture/selection.md` (Architecture A selected)
  - `docs/PRD.md`
  - `docs/design/game-design-core.md`
  - `docs/design/level-difficulty.md`
  - `docs/design/scoring-replayability.md`

## 1) v1 Scope Lock

Scope lock is based on Architecture A delivery constraints (host-authoritative NGO + Relay, cross-platform quality tiers, fixed content cap) and PRD release criteria.

### Must-Have (v1 ship blockers)
1. **Core gameplay loop + controls**
   - Fixed-arc shot model (tap/short hold/long hold), no manual turret tilt.
   - Readable shot feedback (landing marker states, short miss replay).
   - Fast checkpoint restart path (<2s target from design doc).
2. **Combat puzzle systems**
   - Destructible classes required for puzzle outcomes (brittle, reinforced, explosive/chain, support-collapse).
   - Ramp/elevation traversal integrated into puzzle solves.
   - Host-authoritative puzzle state resolution.
3. **Playable roster baseline**
   - Player tanks: Blue + Green with distinct stat/solve profiles.
   - Enemy baseline: Scout, Bruiser, Sniper + final boss implementation aligned to map bosses.
4. **Progression content cap**
   - 3 maps × 5 nodes (15 total), with final node boss per map.
   - Star-based unlock flow and world gating logic.
5. **Multiplayer MVP**
   - 2-player co-op host-authoritative flow.
   - Create/join, scene sync, match-end summary.
   - State consistency for destructibles/triggers/pickups/objectives.
6. **Core UX + settings + accessibility floor**
   - Required menus from PRD (boot/main/mode/tank/world map/HUD/pause/results/settings).
   - Input presets by platform; basic remapping where supported.
   - Audio sliders + graphics presets + minimum accessibility set (contrast/colorblind presets, UI scale, camera shake toggle, subtitles).
7. **Release gate readiness**
   - Platform smoke coverage for WebGL, Mobile, Xbox profiles.
   - QA + playtest sign-off criteria from DoD and workflow gates.

### Should-Have (include unless schedule risk materializes)
1. **Fourth enemy utility behavior** (Engineer/Saboteur behavior package) beyond baseline trio.
2. **Branch node skip tuning polish** for star recovery loops.
3. **Expanded replay hooks**
   - Rich results lane breakdown callouts.
   - Co-op shared combo tuning pass.
4. **Assist tuning polish**
   - Fine-grained aim assist strengths + retry aid balancing.
5. **Telemetry depth v1.1-ready**
   - Additional balance dashboards beyond minimum event hooks.

### Later (explicitly de-scoped from v1)
1. Dedicated servers / host migration.
2. 3–4 player production-hardening (beyond extensibility readiness).
3. Full live-ops stack (economy, seasons, ranked anti-cheat depth).
4. UGC/editor workflows.
5. Cutscene-heavy narrative campaign layer.
6. Full ghost rival + weekly circuit feature-complete programs (keep only minimal hooks if time permits).

---

## 2) Risk-Adjusted Schedule Slices

Planning baseline uses **10 nominal build weeks + 3 weeks risk reserve** (high-risk areas: multiplayer sync, cross-platform perf, content throughput).

| Slice | Focus | Nominal | Risk Buffer | Risk Level | Exit Criteria |
|---|---|---:|---:|---|---|
| S0 | Foundation/vertical slice setup (Architecture A baseline, module scaffolding, debug event IDs/hash checks) | 1w | 0.5w | Med | Build boots with module boundaries + instrumentation in place |
| S1 | Core tank/puzzle loop (movement, fixed-arc shots, destructibles, ramps, checkpoint reset) | 2w | 0.5w | Med | Single internal test arena proves full solve loop deterministically |
| S2 | Multiplayer authoritative stack (Lobby/Relay, host sim, 2P join/sync/end flow) | 2w | 1.0w | High | 2P vertical slice with stable puzzle state sync across retries |
| S3 | Content pass I (Map 1 complete + Map 2 nodes 2-1..2-3) | 2w | 0.5w | Med | 8 shippable nodes with stars/unlock + tank viability checks |
| S4 | Content pass II (Map 2 finish + Map 3 + boss integration) | 2w | 1.0w | High | All 15 nodes playable end-to-end with boss phase checkpoints |
| S5 | Scoring/replay + platform fit (results breakdown, settings/accessibility pass, perf budgets Web/mobile/Xbox) | 1w | 0.5w | Med | Score lanes visible + platform quality profiles stable in smoke tests |
| S6 | QA/playtest hardening + launch gate artifacts | 0w (integrated) | 1.0w | High | TPA-022/023 validation package complete; no P0/P1 blockers |

**Critical-path risks to actively burn down early:**
- Multiplayer desync in destructible puzzle states.
- Performance variance across WebGL/mobile/Xbox with readability preserved.
- Content authoring throughput for 15 nodes with two valid solve styles (Blue/Green expression).

---

## 3) Dependency Map

### Workflow Gate Dependencies
- `TPA-005 Architecture Selection` ✅ → enables `TPA-021 PM Triage` ✅.
- `TPA-021` → unblocks parallel ticket proposals (`TPA-009/010/011`).
- `TPA-009/010/011` → `TPA-012` master backlog lock.
- `TPA-012` → implementation tracks (`TPA-013/014/015`).
- `TPA-013/014/015` → `TPA-022` implementation milestone.
- `TPA-022` → `TPA-016/017` validation tracks.
- `TPA-016/017` → `TPA-023` validation sign-off.
- `TPA-023` → `TPA-018/019` promotion/comms.

### Feature/System Dependencies (Build Order)
1. **Core sim first:** fixed-arc + destructible authority model is prerequisite for level scripting, enemy tuning, and scoring fairness.
2. **Network authority before full content scale:** puzzle state replication rules must be stable before Map 2/3 production lock.
3. **Progression data model before final node tuning:** star gates and unlock schema must exist before balancing campaign flow.
4. **Telemetry/event IDs before scoring polish:** replayability tuning requires reliable event capture and breakdown visibility.
5. **Platform profile configs before content freeze:** VFX/tick/quality tiers must be validated early to avoid late redesign.

### Parallelizable Tracks (after S2)
- Content production (maps/nodes) can run parallel with UX/settings polishing.
- Boss encounter scripting can run parallel with scoring/replay tuning once core combat events are stable.
- QA automation/checklists can start on Map 1 while Maps 2/3 are still in development.

---

## 4) Acceptance Criteria by Feature Slice

## S0 — Foundation & Architecture Baseline
- Unity 2022.3 LTS + URP project modules aligned to Combat/Puzzle/AI/Networking/UI/Progression/Save.
- NGO + Relay + Lobby integration compiles and can host/join local test session.
- Debug-only event IDs (`TickId`) and periodic state hash logging available in dev builds.

## S1 — Core Combat Puzzle Loop
- Player can complete at least one designed puzzle room using only fixed-arc shot bands and positioning.
- Destructible interactions change objective state deterministically.
- Checkpoint restart returns player to valid state in <2s on target test rig.
- Blue and Green stat profiles feel materially distinct without control remap changes.

## S2 — Multiplayer MVP Reliability
- Two players can complete a full encounter with synchronized destructibles/triggers/pickups.
- Host-authoritative outcomes are consistent under normal network jitter profile.
- Session create/join/rejoin-after-end flow is stable with clear error UX.
- Match-end summary displays completion + stars consistently for both peers.

## S3 — Campaign Progression Foundation
- Map 1 fully playable (nodes 1-1..1-5 including boss).
- Minimum 3 stars logic implemented and persisted.
- World map node unlock flow allows progression with 1-star clears.
- Optional branch-node behavior for star recovery implemented at least once.

## S4 — Full Content Completion
- All 15 nodes playable end-to-end with no missing blockers.
- Map bosses (1-5, 2-5, 3-5) include phase transitions and fair telegraphs.
- Both Blue and Green can clear every critical objective path without hard lock.
- Enemy roster supports required pacing phases (Read → Pressure → Checkmate).

## S5 — Scoring, Replayability, Platform Fit
- Results screen shows lane breakdown (Efficiency/Style/Combo/Objectives).
- Score floor rule enforced (`FinalScore >= ClearBase`, no negative total).
- Platform presets validated for WebGL/mobile/Xbox with equivalent puzzle readability.
- Required settings + accessibility floor implemented and persisted.

## S6 — Validation & Launch Readiness
- QA report: no open P0/P1 defects.
- Playtest report confirms puzzle clarity, fair ramp, and retry motivation.
- Multiplayer consistency checks pass for gameplay-critical state.
- Workflow artifacts for milestone gates are linked and complete.

---

## 5) Triage Decision Outcome

**TPA-021 is complete.** v1 scope is locked to a co-op-first, host-authoritative campaign delivering 15-node progression with fixed-arc puzzle combat, two player classes, baseline red enemy roster + bosses, and cross-platform settings/accessibility minimums. Advanced replay/live-ops and server-scale features are intentionally deferred to post-v1.
