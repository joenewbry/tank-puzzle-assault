# TPA-026 / PM2-DEV5 Hotfix: Damage & Collision Pipeline Unification

## Goal
Unify projectile hit handling across **player**, **enemy**, and **block/destructible** targets with clear extension hooks for:
- shield interception
- future power-up damage modifiers

## What changed

### New combat pipeline (`Unity/Assets/Scripts/Combat`)
- `ProjectileHitContracts.cs`
  - Shared hit models and interfaces:
    - `IProjectileDamageable`
    - `IProjectileShield`
    - `IProjectileDamageModifier`
- `ProjectileHitResolver.cs`
  - Centralized hit resolution flow:
    1. collect projectile/instigator/target damage modifiers
    2. apply modifiers by priority
    3. run optional shield absorb
    4. apply final damage to unified receiver
- `ProjectileController.cs`
  - Single collision entrypoint (`OnCollisionEnter`/`OnTriggerEnter`) that routes through resolver
- `ProjectileDamageReceiver.cs`
  - Unified receiver for player/enemy/block
  - Supports forwarding to:
    - `EnemyTankAI`
    - `BossTankController`
    - `Core.DestructibleObject`
    - legacy gameplay destructible component
  - Includes fallback local health pool (useful for player prototypes)
- `ProjectileShield.cs`
  - Reusable shield absorb implementation
- `DamageMultiplierModifier.cs`
  - Example modifier component for future power-up effects

### Compile-safety and integration fixes
- `Core/ProjectileArcSolver.cs`
  - added required imports for `Vector3` and `List<>`
- `Gameplay/ProjectileArcSolver.cs`
  - corrected ballistic solve and path generation
  - aligned with `IProjectileArcSolver`
- `Gameplay/DestructibleObject.cs`
  - moved into `Gameplay.Legacy` namespace to remove duplicate global `DestructibleObject` conflict
- `Gameplay/PowerupBox.cs`
  - shield pickup now recharges/creates `ProjectileShield`
  - health pickup now restores through `ProjectileDamageReceiver`

## Usage in scene/prefabs
1. Add `ProjectileDamageReceiver` to player, enemy, and destructible/block prefabs.
2. Add `ProjectileShield` where shield behavior is desired.
3. Add `ProjectileController` to projectile prefab and initialize with instigator + base damage.
4. Optionally add `DamageMultiplierModifier` on source/target to tune damage.

## Notes
- This patch focuses on unifying collision/damage flow and extension seams, not full gameplay balancing.
- Existing scripts remain compatible through receiver forwarding and legacy destructible support.
