# TPA-014: Core Gameplay Mechanics Implementation

## Overview

This document describes the implementation of core gameplay mechanics for Tank Puzzle Assault (TPA-014), including projectile trajectory, tank control, destructible environments, ramp physics, and power-up systems.

## ProjectileArcSolver

- Implements physics-based arc calculation using gravity, initial velocity, and target position.
- Uses trigonometric solution to determine optimal launch angle for a given speed.
- Returns normalized direction vector for projectile instantiation.
- Assumes flat horizontal plane; vertical offset (y) is handled via discriminant adjustment.

## TankControllerBase

- Abstract base class for all tank types (player, AI, enemy).
- Handles movement (speed, turning), shooting (cooldown, barrel spawn), and target acquisition.
- `RotateTowardTarget()` uses Quaternion.Slerp for smooth rotation.
- `Shoot()` instantiates projectile with combined velocity (tank + barrel direction).
- Designed to be extended by specific tank subclasses (e.g., `PlayerTank`, `AITank`).

## DestructibleObject

- Manages health and destruction logic for terrain and objects.
- On health ≤ 0: plays particles, sound, spawns 8 randomized debris chunks with explosive force.
- Uses `Destroy(gameObject)` to clean up after destruction.
- Prevents multiple destruction triggers with `isDestroyed` flag.

## RampTile

- Defines sloped terrain tiles that affect movement and physics.
- Provides methods to return slope multiplier (`GetSlopeMultiplier()`) and friction reduction (`GetFrictionMultiplier()`).
- `ApplyRampEffect()` is a virtual method intended for override in tank controllers to apply directional velocity adjustments.
- Does not modify physics directly; instead signals to controllers how to adapt.

## PowerupBox

- Spawns randomized power-ups on destruction (Health, Speed, FireRate, Armor).
- Uses `Resources.Load` to instantiate prefabs from `Assets/Resources/Prefabs/Powerups/`.
- On destruction: plays explosion animation and particles, then spawns one power-up.
- Prevents multiple pickups with `isCollected` flag.
- Any damage triggers destruction (simple design).

## Integration Notes

- All scripts are designed for Unity 2022 LTS and use Rigidbody-based physics.
- No networking code included — intended for single-player first, extensible to multiplayer later.
- Powerup prefabs must be pre-created in `Assets/Resources/Prefabs/Powerups/`.
- DestructibleObject and PowerupBox expect appropriate particle systems and audio sources on their GameObjects.

## Future Extensions

- Add network synchronization (Netcode for GameObjects)
- Add visual indicators for ramp slopes (e.g., arrow decals)
- Add power-up stacking or duration timers
- Add audio cues for low health or critical power-ups
