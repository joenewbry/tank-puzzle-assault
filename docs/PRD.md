# Product Requirements Document (PRD)
# Tank Puzzle Assault

- **Project:** tank-puzzle-assault
- **Owner:** Product Management
- **Date:** 2026-03-03
- **Status:** Draft for execution

## 1) Product Summary
Tank Puzzle Assault is a Unity-based puzzle-action tank game where players solve combat puzzles using fixed-arc shots (no turret tilt), destructible environments, ramp traversal, and class-based tank play.

The v1 release targets:
- **Web (WebGL)**
- **Mobile (iOS/Android)**
- **Xbox**

Core promise: *"Think like a puzzle player, act like a tank commander."*

## Current Priority Mode (GM Direction Update — 2026-03-03)

**Priority mode is now single-player campaign first.** This direction supersedes any earlier sequencing that treated multiplayer as a co-equal v1 gate.

- **Primary execution target:** ship a polished, playable single-player puzzle campaign loop first (on top of existing prototype systems).
- **Content strategy:** leverage already imported world/environment assets as the default content source for campaign levels; only create net-new assets when blockers are identified.
- **Multiplayer policy:** keep architecture compatibility in mind, but defer multiplayer completion/hardening work (session flow, sync robustness, balance/perf tuning, broader player counts) to a later phase after campaign MVP acceptance.
- **Planning implication:** implementation and QA sequencing should prioritize campaign progression clarity, puzzle readability, and single-player stability before returning to multiplayer hardening.

---

## 2) Goals and Success Criteria

## Product Goals
1. Deliver a complete 3-map progression campaign with escalating difficulty.
2. Ship a stable, polished single-player puzzle campaign loop first.
3. Preserve multiplayer-compatible architecture while deferring multiplayer hardening to a later phase.
4. Provide replayable challenge through scoring, star ratings, and class/power-up variety.
5. Ship platform-appropriate controls, settings, and performance profiles on web/mobile/Xbox.

## Success Criteria (v1 Campaign MVP)
- 15 playable nodes (3 maps x 5 nodes) with clear unlock progression.
- 2 player tank classes + red AI variants + 1 final boss class implemented.
- Core menus/settings complete and platform-compliant for single-player campaign flow.
- QA/playtest sign-off on puzzle clarity, fairness, and stability in single-player progression.
- Multiplayer hardening backlog is explicitly defined, prioritized, and scheduled for post-campaign-MVP phase.

---

## 3) Scope (In Scope)

## 3.1 Core Gameplay
- Fixed-arc shooting system (tap/charge power bands, no manual barrel tilt).
- Tank movement and aim positioning as primary puzzle-solving skill.
- Puzzle-combat encounters combining:
  - Destructibles
  - Ramps/elevation lanes
  - Cover and timing windows
  - Pickups (health + mystery + power-up effects)

## 3.2 Tank Classes
- **Blue Tank (precision/tempo):** faster reload, lower splash, stronger precision and bounce lines.
- **Green Tank (demolition/control):** slower reload, higher splash, stronger destructible pressure.

Both classes must be viable across all core levels with distinct but balanced solve paths.

## 3.3 AI and Enemies
- Red AI variants with readable telegraphs and role-driven behavior:
  - Scout
  - Bruiser
  - Mortar/Artillery
  - Utility/Saboteur/Engineer style variant
- Final Boss tank with multi-phase encounter design.
- AI behavior should combine positional pressure and puzzle interaction (not just raw DPS).

## 3.4 Multiplayer
- **Current priority mode:** deferred to post-campaign-MVP hardening phase.
- Host-authoritative match simulation for gameplay-critical actions.
- MVP multiplayer target: **2-player co-op**, extensible to 3–4 players.
- Session support includes create/join flow, scene sync, match-end summary.
- Multiplayer must preserve puzzle state consistency (destructibles, triggers, pickups, objectives).

## 3.5 Progression and Content
- **Map progression model:** 3 themed worlds, 5 nodes per world (last node boss).
- Unlock logic based on completion + stars.
- Difficulty progression from introductory puzzle-combat to advanced combined-arms boss encounters.

Proposed map arc:
1. **Map 1 (Intro):** arc fundamentals + basic destructibles.
2. **Map 2 (Intermediate):** layered puzzle sequences + mixed enemy pressure.
3. **Map 3 (Advanced):** constrained resources + multi-phase final boss.

## 3.6 Destructibles and Environment Rules
- Destructible classes: brittle, reinforced, explosive/chain, support-collapse.
- Destruction must have clear gameplay consequences:
  - Open/close routes
  - Trigger puzzle state changes
  - Reveal weak points/objectives
- Debris/VFX can be cosmetic, but puzzle state resolution must be authoritative and deterministic enough for multiplayer consistency.

## 3.7 Menus and Required Settings

### Required Menus
- Splash/Boot
- Main Menu
- Mode Select (Campaign/Multiplayer)
- Tank Select
- World Map / Level Select
- In-Game HUD
- Pause Menu
- Results Screen
- Settings Menu

### Required Settings
- Controls/keybind remapping (where platform allows)
- Input presets by platform (KBM, touch, gamepad)
- Audio sliders (Master/Music/SFX/UI/Voice)
- Graphics quality preset (Low/Med/High/Auto)
- Accessibility options:
  - Color contrast / colorblind presets
  - UI scale
  - Camera shake toggle/intensity
  - Aim assist visibility/tuning
  - Subtitle options

---

## 4) Non-Goals (Out of Scope for v1)
- Dedicated server architecture requirement at launch.
- Full live-ops economy/monetization design beyond baseline scoring/challenges.
- User-generated levels or map editor.
- Narrative campaign with cutscene-heavy story delivery.
- Esports-grade ranked mode and anti-cheat stack.

---

## 5) Platform Targets and Constraints

## Web (WebGL)
- Prioritize broad browser compatibility and stable framerate.
- Lower visual complexity and networking tick profile as needed.

## Mobile
- Touch-first controls with optional controller support.
- Battery/thermal-aware performance settings.

## Xbox
- Gamepad-first UX defaults.
- Certification-conscious handling of pause, session errors, and settings persistence.

Cross-platform requirement: core progression and puzzle readability must remain equivalent, even if fidelity differs.

---

## 6) High-Level Requirements by Discipline

## Product/Design
- Finalize map node sequence and objective taxonomy.
- Define difficulty targets and assist options.
- Provide class balance targets and counterplay matrix.

## Engineering
- Implement authoritative puzzle/combat state handling.
- Deliver platform input abstraction and settings persistence.
- Build progression save structure and match/session flow.

## QA/Playtest
- Validate mission clarity, fail/retry friction, and difficulty ramp.
- Validate multiplayer synchronization for destructibles/objectives.
- Verify platform-specific controls/settings behavior.

## Growth/Comms
- Promotion plan aligned to feature milestones.
- Milestone email templates for internal/external stakeholders.

---

## 7) Dependencies and Workflow Gates
- Architecture selection gate before implementation ticket finalization.
- PM triage gate before master backlog lock.
- Implementation completion gate before QA/playtest sign-off.
- QA/playtest sign-off gate before promotion/comms launch execution.

(Tracked in `planning/workflow.csv`.)

---

## 8) Risks and Mitigations
- **Risk:** Cross-platform input parity hurts puzzle fairness.  
  **Mitigation:** Platform-specific control presets + assist tuning + playtest matrix.
- **Risk:** Multiplayer desync on destructible puzzle state.  
  **Mitigation:** Host authority + deterministic puzzle state transitions.
- **Risk:** Scope creep from level content.  
  **Mitigation:** Fixed v1 content cap (15 nodes) and strict out-of-scope list.
- **Risk:** Difficulty spikes reduce retention.  
  **Mitigation:** Star-based progression, optional assists, checkpoint pacing.

---

## 9) Release Readiness for v1
v1 is release-ready only when:
1. PRD scope requirements are implemented and verified.
2. Definition of Done criteria are met (`planning/definition-of-done.md`).
3. Workflow gates are complete with artifact links in CSV.
4. Promotion + milestone comms assets are approved.
