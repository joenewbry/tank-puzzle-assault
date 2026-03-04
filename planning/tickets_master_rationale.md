# TPA-012 Consolidation Rationale — `tickets_master.csv`

Date: 2026-03-03  
Source inputs: `tickets_pm1.csv`, `tickets_pm2.csv`, `tickets_pm3.csv`, `triage-summary.md`, `docs/architecture/selection.md`

## Consolidation strategy

I merged the three PM proposals into one execution-grade backlog by prioritizing:
1. **First playable in 2–3 hours** (W0) with three coding agents running in parallel.
2. **Architecture A compliance** (host-authoritative NGO + Relay, module seams, cross-platform readiness).
3. **Risk burn-down first** (multiplayer sync, puzzle determinism, diagnostics/CI hooks).
4. **Clear ownership split** between coding (`arcade-dev-01/02/03`), design decisions (`tech-architect`), and non-code execution (`arcade-pm`, support agents).

## What was taken from PM1

PM1 supplied the **most complete feature inventory** and hard v1 requirements.
- Kept: project scaffold/module seams, fixed-arc combat, destructibles, ramps, pickups, Blue/Green classes, multiplayer sync scope, required UI flow, diagnostics, and deployment readiness.
- Reduced: broad multi-map/full-boss production tasks were trimmed out of W0 and moved to W1+ sequencing to keep immediate slice feasible.
- Why: PM1 is strong on completeness, but too large to execute immediately without slicing.

## What was taken from PM2

PM2 supplied the best **risk-driven milestone logic** and operational hardening gates.
- Kept: diagnostics-first mindset, host-authoritative simulation spine, desync soak thinking, platform CI matrix, milestone gate framing.
- Reduced: long M4/M5 launch-hardening items not needed for immediate playable slice.
- Why: PM2 is best at preventing late surprises; selected items were pulled earlier where they protect W0 execution.

## What was taken from PM3

PM3 supplied the cleanest **vertical-slice-first dependency sequence**.
- Kept: W0 order (foundation → networking → combat/puzzle → content/UI → gate), and near-term hardening follow-ups.
- Reduced: deeper hardening sequence was condensed into W1 backlog anchors.
- Why: PM3 is the best execution skeleton for a fast playable build.

## How triage + architecture selection changed the final backlog

- `selection.md` locked **Architecture A**, so all tickets enforce host authority, service seams, and platform profile awareness.
- `triage-summary.md` locked v1 must-haves and risk areas, so W0 includes:
  - fixed-arc loop,
  - authoritative puzzle state,
  - 2P join/sync/disconnect sanity,
  - minimal UI loop,
  - Map 1-1 playable encounter,
  - gate + QA/playtest evidence.

## Resulting master backlog shape

- **W0 (0–3h):** executable first playable slice with core systems and evidence capture.
- **W1 (3–6h):** immediate hardening (diagnostics overlay, CI matrix) and AI slice extension.
- **Single source of truth:** `planning/tickets_master.csv` now drives implementation handoff for TPA-013/014/015.
