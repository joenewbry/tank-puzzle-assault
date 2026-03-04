# TPA-014: Core Gameplay Mechanics Implementation

## Overview

This document details the implementation of core gameplay mechanics for the Tank Puzzle Assault game.

## Projectile Arc Solver

- Calculates projectile trajectory using physics-based quadratic equation solving
- Supports two solution angles (high/low arc) with preference for lower arc
- Includes impact prediction with simplified gravity simulation
- Uses `Vector3` for 3D space calculations with X/Z as horizontal plane

## Tank Controller Base

- Abstract base class for all tank controllers
- Implements core movement (WASD/arrow keys)
- Mouse-aim rotation toward camera's cursor position
- Cooldown-based shooting system
- Uses Rigidbody for physics-based motion

## Destructible Object

- Health-based destruction system
- Supports customizable destruction effects and sounds
- Instantiates particle effects and plays audio on destruction
- Destroys GameObject upon health reaching zero

## Ramp Tile

- Defines sloped terrain with configurable angle
- Computes corner positions based on transform scale and orientation
- Provides basic walkability check (placeholder for physics integration)
- Uses trigonometry to adjust vertical height based on slope

## Powerup Box

- Animated floating behavior using sine wave offset
- Type-based powerup system (Health, Shield, SpeedBoost, MultiShot)
- Trigger-based pickup via Unity Collider system
- Randomized float phase for visual variety

## Integration Notes

- All scripts are placed under `Unity/Assets/Scripts/Gameplay/`
- Physics are handled by Unity's built-in Rigidbody system
- Input is handled via Unity's legacy Input Manager
- Collision detection uses Unity's trigger system
- No external dependencies beyond Unity 2022 LTS

## Future Work

- Replace legacy input with Unity's new Input System
- Integrate NavMesh for AI pathfinding on ramps
- Add particle system presets for powerups
- Implement network synchronization for multiplayer