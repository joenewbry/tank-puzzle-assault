# Tank Puzzle Assault — Definition of Done (DoD)

- **Project:** tank-puzzle-assault
- **Date:** 2026-03-03
- **Owner:** PM

This DoD defines the minimum completion bar for planning, implementation readiness, validation, and launch preparation.

---

## 1) Planning Done
Planning is DONE when all are true:
- [ ] PRD exists at `docs/PRD.md` and covers scope, non-goals, platforms, multiplayer/AI, progression, classes, destructibles, menus/settings.
- [ ] Architecture selection document exists at `docs/architecture/selection.md` with rationale.
- [ ] Master backlog exists at `planning/tickets_master.csv` with prioritized, dependency-linked tasks.
- [ ] `planning/workflow.csv` reflects current status, dependencies, and artifact paths.

---

## 2) Feature/Implementation Done
Implementation is DONE when all are true:
- [ ] Core tank controls and fixed-arc shooting are functional.
- [ ] Destructible systems and ramp traversal are functional in gameplay.
- [ ] Blue and Green player tank classes are implemented and selectable.
- [ ] Red enemy variants and final boss are implemented with readable telegraphs.
- [ ] Multiplayer host/client flow is playable and stable for target MVP player count.
- [ ] 3 maps x 5 nodes progression is implemented with unlock logic.
- [ ] Required menus and settings are implemented across target platforms.

---

## 3) QA / Playtest Done
Validation is DONE when all are true:
- [ ] QA pass completed with no P0/P1 blockers open.
- [ ] Playtest report confirms puzzle readability, fair difficulty ramp, and replay motivation.
- [ ] Multiplayer consistency verified for destructibles/objectives/pickups.
- [ ] Platform smoke tests pass on web, mobile, and Xbox target profile.
- [ ] Accessibility and settings sanity checks complete (input, audio, visuals, UI scale/subtitles).

---

## 4) Launch Prep Done
Launch prep is DONE when all are true:
- [ ] Promotion plan documented at `docs/launch/promotion-plan.md`.
- [ ] Milestone email templates documented at `docs/comms/email-templates.md`.
- [ ] Release/PR flow documented and accepted.
- [ ] Known issues triaged with owner + plan.

---

## 5) Project Done (Release Candidate)
Project is DONE when all are true:
- [ ] Sections 1–4 above are fully satisfied.
- [ ] Workflow milestones for architecture, PM triage, implementation, QA/playtest, promotion, and comms are complete.
- [ ] Final go/no-go review recorded by PM + stakeholders.

If any item above is not satisfied, release remains **NOT DONE**.
