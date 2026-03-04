# TPA-005 Architecture Selection — v1 Delivery

Date: 2026-03-03
Owner: PM Reviewer (TPA-005)
Decision: **Select Architecture A** for v1 implementation baseline.

## 1) Executive Summary

After reviewing proposals A/B/C against v1 delivery priorities, **Architecture A is the winner**.

A provides the best balance of:
- fast implementation using proven Unity stack (NGO + Relay + host authority),
- manageable technical risk,
- credible multiplayer behavior for a co-op-first MVP,
- practical cross-platform execution (Web/mobile/Xbox) with concrete quality/tick constraints.

Architecture B is strongest on long-term simulation rigor, but too complex for v1 schedule/risk tolerance. Architecture C is a strong runner-up on productization and extension seams, but A is more implementation-ready and operationally concrete for immediate delivery.

## 2) Weighted Score Table

Scoring scale: 1 (poor) to 5 (excellent).  
Weighted score formula: `score × weight`. Max total = 500.

| Criterion | Weight | A | B | C |
|---|---:|---:|---:|---:|
| Shipping speed | 30 | 4.5 (135) | 2.5 (75) | 4.0 (120) |
| Technical risk (lower risk = higher score) | 25 | 4.0 (100) | 2.0 (50) | 3.5 (87.5) |
| Multiplayer reliability | 20 | 3.5 (70) | 4.5 (90) | 3.5 (70) |
| Cross-platform feasibility | 15 | 4.5 (67.5) | 3.5 (52.5) | 4.0 (60) |
| Extensibility | 10 | 3.5 (35) | 4.5 (45) | 4.5 (45) |
| **Total** | **100** | **407.5 / 500 (81.5%)** | **312.5 / 500 (62.5%)** | **382.5 / 500 (76.5%)** |

## 3) Why the Winner Was Selected

**Architecture A wins because it is the most v1-executable plan with acceptable reliability and risk.**

Key reasons:
1. **Most concrete delivery path:** clear package choices, authority model, content plan, and phased build timeline.
2. **Strong v1 fit:** host-authoritative NGO + Relay is sufficient for co-op-first launch without building dedicated server infra now.
3. **Cross-platform realism:** explicit handling for WebGL/mobile/Xbox quality/tick constraints.
4. **Scope control:** includes practical mitigations for puzzle/network/asset risks and avoids over-engineering.
5. **Team usability:** clear folder structure and implementation decomposition are immediately actionable for engineering.

## 4) Ideas from Non-Winning Proposals That Are Still Adopted

### Adopted from Architecture B
- **Event discipline for multiplayer debugging:** include monotonic `TickId`/event IDs on critical gameplay events.
- **Deterministic diagnostics (debug builds):** periodic lightweight state hash checks to catch desync early.
- **Strict gameplay-vs-presentation separation:** gameplay outcomes never depend on cosmetic physics.

### Adopted from Architecture C
- **Interface seams around networking/services:** introduce `INetworkRuntime`-style abstraction to ease future dedicated-server migration.
- **Data-first tuning:** keep combat/power-up/balance in ScriptableObjects; allow controlled remote tuning later.
- **Light telemetry hooks from day one:** minimal event bus for balancing and stability metrics.

## 5) Explicit v1 Architecture Baseline to Implement

The implementation baseline for v1 is:

1. **Engine/Runtime**
   - Unity 2022.3 LTS + URP.
   - Input System, Addressables, Cinemachine.

2. **Networking/Authority**
   - Netcode for GameObjects + Unity Transport.
   - UGS Lobby + Relay.
   - **Host-authoritative** simulation for combat, puzzle state, pickups, destructibles, and AI.
   - No host migration in v1 (graceful end/rejoin flow).

3. **Gameplay Simulation Rules**
   - Fixed-arc weapon profiles (no manual tilt).
   - Host-resolved projectile/hit/puzzle trigger outcomes.
   - Client prediction only for responsiveness/visual smoothing.

4. **Game Content Scope (v1 locked)**
   - Player tanks: Blue, Green.
   - Enemies: Red Scout, Red Bruiser, Red Sniper + final boss.
   - Systems: destructibles, ramps, health box, mystery box, power-ups.
   - Progression: 3 maps, 15 stages total, star-based node unlock flow.

5. **Architecture/Code Organization**
   - Keep third-party assets read-only; use wrapper prefabs/variants.
   - Use module boundaries aligned to Combat, Puzzle, AI, Networking, UI, Progression, Save.
   - Add lightweight service interfaces to avoid hard-coupling to current online stack.

6. **Quality and Platform Baseline**
   - Web/mobile/Xbox quality tiers and tick/VFX budgets as first-class config.
   - Early perf and sync validation in Map 1 vertical slice.
   - Add debug-only desync diagnostics (event IDs + periodic state hash).

This baseline is now the source architecture for downstream planning/ticket decomposition.
