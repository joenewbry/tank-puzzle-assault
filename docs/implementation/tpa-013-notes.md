# TPA-013: Unity Scaffold Implementation Notes

## Scaffold Decisions

- **Project Structure**: Follows standard Unity 2D project hierarchy with clear separation of concerns:
  - `Scripts/Core`: Bootstrapping and configuration
  - `Scripts/Gameplay`: Core mechanics (movement, shooting, collision)
  - `Scripts/AI`: Enemy behavior trees and state machines
  - `Scripts/UI`: HUD, menus, and overlay systems
  - `Prefabs/` and `Scenes/`: Reusable assets and scene layout

- **Code Style**: Pure C# with no external dependencies. All scripts are MonoBehaviour-based for editor compatibility but remain compilable in CLI environments.

- **MatchConfig**: Serializable class for runtime configuration. Designed to be extensible (JSON/YAML import future-proof).

- **GameBootstrap**: Singleton-style initializer. No scene management logic yet—intentionally minimal for now.

## Next Handoff Points

1. Implement `TankMovement` and `TankShooter` in `Scripts/Gameplay`
2. Add `EnemyAI` prototype in `Scripts/AI` (patrol + chase behavior)
3. Create `UI/` scripts: Health bar, score, round timer
4. Build `Main.unity` scene with camera, player spawn point, and test ground
5. Add prefab for player tank (simple cube + collider)

## Build Notes

- No Unity Editor required to validate compilation (C# scripts are plain)
- All assets can be created in-editor or via asset pipeline later
- This scaffold is designed to be imported into any Unity 2022.3+ project
