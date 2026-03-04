# TPA-015: AI Progression System Implementation

## Overview

This document details the implementation of the AI progression system for *Tank Puzzle Assault*, including:
- A state machine for baseline enemy behavior
- A variant configuration model for enemy types
- A boss phase system with threshold-driven behavior changes
- Three-map progression with node metadata

## AI State Machine (EnemyTankAI.cs)

The `EnemyTankAI` script implements a finite state machine (FSM) with the following states:

- `Idle` → Transition to `Patrolling`
- `Patrolling` → Enter `SeekingPlayer` if player is within `chaseRadius`
- `SeekingPlayer` → Enter `Attacking` if player is within `attackRadius`; revert to `Patrolling` if player escapes beyond `chaseRadius * 1.5f`
- `Attacking` → Fire projectile on cooldown; revert to `SeekingPlayer` if player moves out of `attackRadius * 1.5f`
- `Fleeing` → Triggered under low health; reverts to `Patrolling` when far from player
- `Dead` → Final state; disables movement and triggers death animation

Movement uses `Rigidbody2D` with smooth rotation toward target direction. Patrol points are generated randomly within `patrolRadius`.

## Variant Configuration Model (EnemyVariantConfig.cs)

The `EnemyVariantConfig` class defines serializable templates for enemy types, including:

- Core stats: health, speed, damage, cooldown, radii
- Behavior flags: `canFlee`, `isBoss`
- Visual/audio assets: sprite, attack/death sounds
- Special abilities: shield, AoE attack, dash
- Progression metadata: `unlockLevel`, `unlockWave`, `spawnWeight`

Default values are set in constructor to simplify editor configuration.

## Boss Phase System (BossTankController.cs)

The `BossTankController` manages three progressive phases triggered by health thresholds:

| Phase | Health Threshold | Behavior Changes |
|-------|------------------|------------------|
| Phase 1 | ≤70% | +20% speed, +20% radii |
| Phase 2 | ≤40% | +50% speed, +50% radii, AoE attack enabled |
| Phase 3 | ≤10% | +100% speed, +100% radii, dash ability enabled |

Phases are enforced with a minimum duration timer (15s) to prevent rapid transitions. Each phase change triggers:
- Audio cue (from `phaseChangeSounds` array)
- Visual effect (particle system or screen shake)
- AI behavior reconfiguration via `ApplyPhaseBehavior()`

Boss health is tracked separately from the base enemy AI, allowing independent health pools and damage feedback.

## Map Progression Data (MapProgressionData.json)

Three maps are defined, each with:

- `waveCount`: total number of enemy waves
- `nodeProgression`: ordered list of enemy spawn nodes with:
  - `spawnTime`: absolute time in seconds
  - `requiredProgress`: node unlock condition (e.g., 0 = start, 1 = after wave 2, etc.)
  - `enemies`: list of enemy variants with count and spawn delay
- `boss`: defined by variant, spawn wave, and health pool

Node progression is designed to increase enemy variety and density per wave, culminating in the final boss.

## Integration Notes

- All enemy variants referenced in `MapProgressionData.json` must be defined in `EnemyVariantConfig` (e.g., `basic_tank`, `fast_tank`, `boss_tank_phase1`, etc.)
- Boss variants are flagged as `isBoss = true` in their config and are not spawned as regular enemies.
- The `BossTankController` must be attached to the boss GameObject and linked to a reference `EnemyTankAI` component for inherited movement behavior.
- `MapProgressionData.json` is loaded at map start via `Resources.Load` and parsed into a `MapProgressionData` class.

## Future Extensions

- Dynamic difficulty scaling based on player performance
- Customizable AI behavior trees per variant
- AI learning from player patterns (ML-based adaptation)
- Phased boss abilities triggered by time or player actions (not just health)
