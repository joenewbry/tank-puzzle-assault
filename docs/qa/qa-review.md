# TPA-016 QA Review (Code Read)

Date: 2026-03-03
Reviewer: QA Agent (`arcade-agent-06`)
Scope: Static review of `Unity/Assets/Scripts` and `docs/implementation/*` (no runtime playtest in this task)

## Executive Summary

Current implementation has **multiple build/runtime blockers** that should be fixed before deeper QA/playtest:

- At least **2 compile-time blockers** (duplicate type name, missing `using` types)
- At least **2 gameplay-blocking logic defects** (enemy damage/death flow, projectile direction math)
- Several **high-risk null-reference and configuration hazards**
- Cross-platform input/physics approach is currently inconsistent (mix of 2D + 3D assumptions)

---

## Static / Code-Level QA Findings

### Critical blockers

1. **Duplicate `DestructibleObject` type in global namespace (compile failure)**
   - `Unity/Assets/Scripts/Gameplay/DestructibleObject.cs:3`
   - `Unity/Assets/Scripts/Core/DestructibleObject.cs:5`
   - Two top-level types with same name in same namespace will fail compilation (`CS0101`).

2. **`Core/ProjectileArcSolver.cs` references unresolved types (compile failure)**
   - `Unity/Assets/Scripts/Core/ProjectileArcSolver.cs:4-6`
   - `Vector3` and `List<>` are used without `using UnityEngine;` and `using System.Collections.Generic;`.

### High-severity logic defects

3. **Enemy damage is never applied**
   - `Unity/Assets/Scripts/AI/EnemyTankAI.cs:134-143`
   - `TakeDamage(float damage)` never subtracts from `health`; death condition can never be reached in normal flow.

4. **Enemy AI startup can throw NullReference on missing Player-tagged object**
   - `Unity/Assets/Scripts/AI/EnemyTankAI.cs:32`
   - `GameObject.FindGameObjectWithTag("Player").transform` dereferences immediately.

5. **Boss init can throw NullReference when `bossConfig` is unassigned**
   - `Unity/Assets/Scripts/AI/BossTankController.cs:44-47`
   - `enemyAI != null` is checked, but `bossConfig` is not.

6. **Boss health ratio can divide by zero**
   - `Unity/Assets/Scripts/AI/BossTankController.cs:56`
   - `maxHealth` default is unset; division by zero risk if prefab/scene setup misses value.

7. **Projectile arc solver ignores target bearing in horizontal plane**
   - `Unity/Assets/Scripts/Gameplay/ProjectileArcSolver.cs:24-27`
   - Returns direction from `cos/sin(theta)` only, not projected toward target direction vector.
   - Expected result: shots can fly in wrong horizontal direction.

### Medium-severity correctness/consistency issues

8. **Patrol movement is random every frame (jittering pathing)**
   - `Unity/Assets/Scripts/AI/EnemyTankAI.cs:89,118-120`
   - New random point every update means unstable/noisy movement.

9. **Mixed 2D and 3D architecture without clear boundaries**
   - `EnemyTankAI` uses `Rigidbody2D`/`Vector2` (`AI/EnemyTankAI.cs`)
   - `TankControllerBase`/`PowerupBox` use 3D `Rigidbody`/`Collider` (`Gameplay/TankControllerBase.cs`, `Gameplay/PowerupBox.cs`)
   - High risk of collision and movement systems not interacting as intended.

10. **`RampTile.IsWalkable` is placeholder and always true**
    - `Unity/Assets/Scripts/Gameplay/RampTile.cs:35-37`
    - No actual walkability validation.

11. **Phase timing behavior is documented but not enforced**
    - `Unity/Assets/Scripts/AI/BossTankController.cs:77-83`
    - `UpdatePhaseTiming()` currently no-op; implementation doc claims minimum phase duration behavior.

12. **Null-guard gaps for required components**
    - `EnemyTankAI.Attack()/UpdateAnimator()` assumes `Animator` exists (`AI/EnemyTankAI.cs:125,130-131`)
    - `TankControllerBase.HandleInput()` assumes `Camera.main` and `Rigidbody` exist (`Gameplay/TankControllerBase.cs:14-15,30`)

---

## Probable Runtime Risks

1. **Build does not pass in CI/editor due to compile blockers**
   - Duplicate `DestructibleObject`
   - Missing `using` declarations in core arc-solver interface

2. **Combat loop soft-lock risk**
   - Enemies may never die (`TakeDamage` bug), preventing wave completion/progression.

3. **Frequent crash-on-load/config fragility in scene setup**
   - Missing Player tag, missing boss config, missing camera/component refs.

4. **Aiming and projectile behavior feels broken/inconsistent**
   - Incorrect arc vector and mixed 2D/3D assumptions can produce non-intuitive shots.

5. **Platform control quality risk (Web/mobile/Xbox)**
   - Input implementation currently desktop legacy-input-centric; touch/controller parity not implemented.

6. **AI movement quality risk**
   - Patrol jitter and placeholder ramp checks can cause erratic, non-believable enemy behavior.

---

## Test Checklist (Web / Mobile / Xbox)

> Run after P0 compile and core gameplay blockers are fixed.

### Shared core smoke (all platforms)

- [ ] Project compiles with zero script errors/warnings in Unity 2022.3+
- [ ] Main scene boots without exceptions in first 30 seconds
- [ ] Player spawn + enemy spawn succeed with valid references
- [ ] Enemy takes damage and reaches `Dead` state
- [ ] Boss transitions through phases and reaches defeat state
- [ ] Projectile trajectory heads toward intended target
- [ ] Powerup pickup trigger works and applies effect
- [ ] Ramp traversal behavior is deterministic and collision-safe

### Web (WebGL)

- [ ] Input latency acceptable with keyboard/mouse
- [ ] Mouse aiming maps correctly at common browser resolutions/aspect ratios
- [ ] No frame-stall spikes during enemy waves/boss phase changes
- [ ] Audio cues work after browser user-interaction gate
- [ ] Memory growth remains bounded across a full match run

### Mobile (iOS/Android)

- [ ] Touch controls available (move + aim + fire) and playable one-/two-thumb
- [ ] UI scaling and readability at small resolutions
- [ ] Stable performance under thermal throttling scenarios (mid-tier devices)
- [ ] Suspend/resume does not break wave/boss state
- [ ] Battery/CPU impact acceptable for one full match

### Xbox (controller)

- [ ] Full controller mapping (move/aim/fire/pause/confirm/back)
- [ ] Aim behavior smooth with stick deadzone tuning
- [ ] Safe-area compliance and UI legibility at TV distances
- [ ] Stable 60 FPS target under combat load
- [ ] Resume-from-suspend and profile sign-in flows do not break game state

---

## Priority Bug List (P0 / P1 / P2)

### P0 (must-fix before next validation gate)

1. Duplicate class name conflict: `DestructibleObject` (Core vs Gameplay)
2. Missing imports/types in `Core/ProjectileArcSolver.cs` interface file
3. `EnemyTankAI.TakeDamage()` does not reduce health (enemy never dies)
4. `EnemyTankAI` null dereference when Player tag not found

### P1 (fix before serious playtest / balance pass)

1. `BossTankController` null guard for `bossConfig` and `maxHealth` validation
2. Projectile arc horizontal direction calculation bug
3. 2D/3D physics and input model mismatch across AI/gameplay scripts
4. Unimplemented phase-duration enforcement despite documented behavior

### P2 (quality/stability hardening)

1. Patrol jitter due to per-frame random destination generation
2. `RampTile.IsWalkable()` placeholder implementation
3. Defensive null/component checks for animator/camera/rigidbody and clearer setup errors
4. Remove/implement unused fields (e.g., `pickupRange`) and placeholder comments

---

## Recommended Next Step

After P0 fixes land, run TPA-017 playtest with a short scripted regression pack covering:
- enemy damage/death loop
- boss transition correctness
- projectile targeting sanity
- cross-platform input sanity (KBM + controller + touch prototype)
