# TPA-022 Implementation Readout

Date: 2026-03-03  
Milestone: TPA-022 (Implementation completion gate after TPA-013/014/015)  
Inputs reviewed: PRs #1/#2/#3, `docs/implementation/tpa-013-notes.md`, `docs/implementation/tpa-014-mechanics.md`, `docs/implementation/tpa-015-ai-progression.md`, `docs/architecture/selection.md`, `planning/tickets_master.csv`, current `Unity/Assets` scripts/data.

---

## 1) What was implemented in TPA-013 / TPA-014 / TPA-015

## TPA-013 — Unity scaffold baseline
Implemented:
- Initial Unity repository structure and baseline script folders (`Core`, `Gameplay`, `AI`, `UI`, `Scenes`, `Prefabs`, `Resources`).
- Core bootstrap/config stubs:
  - `GameBootstrap.cs`
  - `MatchConfig.cs`
  - Core `DestructibleObject` abstraction
  - Core projectile solver interface stub (`IProjectileArcSolver`)
- Documentation handoff note: `docs/implementation/tpa-013-notes.md`.

Outcome:
- Project skeleton exists and downstream implementation tracks were unblocked.

## TPA-014 — Core gameplay mechanics prototype
Implemented:
- `TankControllerBase.cs` (movement, aiming, cooldown-based shooting stub).
- `ProjectileArcSolver.cs` (trajectory and impact helper calculations).
- `DestructibleObject.cs` (health + destruction effect/audio hooks).
- `RampTile.cs` (slope metadata + placeholder walkability).
- `PowerupBox.cs` (pickup enum and trigger flow with placeholder effects).
- Documentation handoff note: `docs/implementation/tpa-014-mechanics.md`.

Outcome:
- Core loop mechanics are represented in code as prototypes, but not yet integrated into a playable scene.

## TPA-015 — AI + boss + progression prototype
Implemented:
- `EnemyTankAI.cs` finite-state-machine baseline.
- `EnemyVariantConfig.cs` serializable variant model.
- `BossTankController.cs` phase-threshold boss logic.
- `Resources/Data/MapProgressionData.json` with 3-map progression content skeleton.
- Documentation handoff note: `docs/implementation/tpa-015-ai-progression.md`.

Outcome:
- AI/progression baseline exists and reflects intended content direction (3 maps, boss phases), but remains mostly data/prototype level.

---

## 2) Current architecture alignment (Winner: Architecture A)

Overall status: **PARTIAL alignment** (good directional scaffold, major MVP systems still missing).

| Architecture A baseline area | Current status | Alignment |
|---|---|---|
| Unity modular structure | Core/Gameplay/AI directories present; some boundaries in place | 🟡 Partial |
| Fixed-arc combat model | Arc solver + tank base present as prototype | 🟡 Partial |
| Destructibles/ramps/pickups | Implemented as local scripts; not authoritative/networked | 🟡 Partial |
| AI variants + boss phases | Baseline FSM/config/phase controller present | 🟡 Partial |
| 3-map progression data | JSON skeleton exists with map/wave/node structure | 🟡 Partial |
| NGO + UTP + Lobby + Relay host-authority stack | Not implemented in code yet | 🔴 Missing |
| Multiplayer-critical authoritative resolution | Not implemented (currently local/prototype) | 🔴 Missing |
| Input System (selected architecture) | Current scripts use legacy `Input.GetAxis` | 🔴 Missing |
| Required scene/menu flow | Not implemented (no functional boot→results flow) | 🔴 Missing |
| Platform/perf/diagnostics guardrails (TickId/hash) | Not implemented | 🔴 Missing |

**Architecture conclusion:** The implementation is aligned with Architecture A at the **prototype building-block** level, but not yet aligned at the **networked vertical-slice** level required for v1 execution.

---

## 3) Known gaps to reach a playable demo

1. **No integrated playable scene loop yet** (spawn/setup/objective/checkpoint/results).
2. **No multiplayer spine** (NGO/Relay create-join-sync-end flow absent).
3. **No host-authoritative puzzle/combat resolution** for destructibles, pickups, projectiles.
4. **Player class system not implemented** (Blue/Green differentiation/selectability missing).
5. **UI flow missing** (main menu, tank select, HUD, pause, results).
6. **Progression system not wired** (JSON data exists but no runtime loader/controller and no star/unlock persistence).
7. **Potential compile/integration blockers**:
   - Duplicate class names (`DestructibleObject`, `ProjectileArcSolver`) across `Core` and `Gameplay` with no namespacing.
   - `Core/ProjectileArcSolver.cs` interface stub is missing required `using` declarations.
   - Mixed 2D/3D physics assumptions (`Rigidbody2D` in AI vs `Rigidbody` in tank base).
8. **Boss/AI telegraph and encounter scripting incomplete** (phase transitions mostly parameter mutation stubs).
9. **No diagnostics/validation hooks** (event IDs, state hash checks, smoke checklist integration).
10. **No platform quality/input profile implementation** for Web/mobile/Xbox targets.

---

## 4) Risks and mitigation next steps

## Key risks
- **R1: Architecture drift risk** (prototype path diverges from selected host-authority architecture).
- **R2: Integration churn risk** (duplicate/overlapping scripts and physics model mismatch cause rework).
- **R3: Multiplayer desync risk** (if authority boundaries not established before content integration).
- **R4: “Looks implemented, not playable” risk** (feature stubs exist without vertical-slice wiring).
- **R5: Schedule compression risk** (UI/progression/networking all still on critical path).

## Immediate mitigations
1. Freeze architecture guardrails (authoritative boundaries, module seams, physics mode choice) before next feature merges.
2. Resolve compile/namespace/duplication issues first to stabilize baseline.
3. Implement networking spine + authority rules before extending content depth.
4. Build one end-to-end Map 1-1 playable slice (solo + 2P) as primary integration target.
5. Add debug diagnostics (event IDs/hash checks) before broader QA/playtest.

---

## 5) Recommended immediate next sprint (Top 10 tasks)

Prioritized for fastest path to a playable, architecture-compliant vertical slice:

1. **Stabilize codebase and remove integration blockers**  
   - Consolidate duplicate classes, add namespaces/usings, pick unified 2D vs 3D physics model.
2. **Publish/lock Architecture A implementation guardrails (ADR-lite)**  
   - Authority boundaries, module ownership, input/network stack decisions.
3. **Implement NGO + UTP + Lobby + Relay bootstrap**  
   - Host create/join/return loop baseline.
4. **Implement host-authoritative projectile + puzzle state resolution**  
   - Projectile validation, destructible/pickup/trigger authority on host.
5. **Deliver Blue/Green player tank classes**  
   - Distinct stat/solve profiles, selectable in runtime flow.
6. **Wire deterministic pickups with host-seeded mystery outcomes**  
   - Race-safe pickup claims and deterministic outcomes.
7. **Build Map 1-1 graybox encounter with checkpoint restart (<2s target)**  
   - Objective completion, fail/retry path, deterministic reset.
8. **Implement minimum playable UI loop**  
   - Main → Tank Select → HUD → Pause → Results-lite.
9. **Execute 2-player co-op sanity pass with match-end + disconnect-safe return**  
   - Verify synchronized critical gameplay state.
10. **Add diagnostics + smoke validation harness**  
   - Tick/Event IDs, host/client state hash probes, and W0 smoke checklist run.

---

## Milestone decision

**TPA-022 gate status: COMPLETE (readout delivered).**  
Implementation tracks TPA-013/014/015 are merged and documented; however, this milestone also confirms that major vertical-slice and networking work remains before QA/playtest gates (TPA-016/017).
