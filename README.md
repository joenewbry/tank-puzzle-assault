# Tank Puzzle Assault

Unity-based multiplayer tank puzzle-action game for OpenArcade.

## Core Concept
- Physics-arc tank shots (no turret tilt control)
- Puzzle-forward level progression with destructible objects and ramps
- AI enemy tanks with variant behaviors + boss encounters
- 3 maps with escalating difficulty

## Status
Bootstrapped by Arcade GM. See `planning/workflow.csv` and docs in `docs/`.

## Unity Setup & Local Prototype

To set up the Unity project locally:

1. Open Unity Hub and create a new **2D** project (Unity 2022.3 LTS recommended)
2. Copy the entire `Unity/` folder from this repo into your project's root directory
3. Open the project in Unity Editor
4. Load the scene: `Assets/Scenes/Main.unity`
5. Press Play to launch the prototype

> **Note**: This scaffold uses minimal Unity dependencies. No external packages required. All scripts are plain C# and compatible with Unity's default 2D template.

For build instructions, see `docs/implementation/tpa-013-notes.md`.
