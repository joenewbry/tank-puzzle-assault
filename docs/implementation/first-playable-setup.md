# First Playable Setup: Map 1-1 Local Demo

## Overview
This document outlines the manual steps to create a playable demo scene in Unity using the foundation scripts from `feature/tpa-027-first-playable-foundation`.

## Prerequisites
- Unity Editor 2022.3 LTS or later
- Open project `/Users/joe/dev/openarcade/tank-puzzle-assault/Unity`

## Steps

### 1. Create a New Scene
1. Go to `File > New Scene` (or `Cmd+N`)
2. Save as `Scenes/Map1-1.unity`

### 2. Set Up the Scene Hierarchy
1. Right-click in Hierarchy → Create Empty → Name it `GameMaster`
2. Right-click → Create Empty → Name it `SpawnPoints`
3. Right-click under `SpawnPoints` → Create Empty → Name it `SpawnPoint_Player`
4. Repeat step 3 to create `SpawnPoint_Enemy_1` through `SpawnPoint_Enemy_4`

### 3. Add Components

#### Attach `GameLoopManager` to `GameMaster`
1. Select `GameMaster`
2. In Inspector → `Add Component`
3. Search for `GameLoopManager` → Add

#### Attach `SimpleSpawnManager` to `GameMaster`
1. Still on `GameMaster`, click `Add Component`
2. Search for `SimpleSpawnManager` → Add
3. Drag `SpawnPoint_Player` to `PlayerPrefab` slot (you’ll create this next)
4. Drag each `SpawnPoint_Enemy_X` to the `SpawnPoints` list (drag and drop 5 total)

> 💡 Tip: Leave `EnemyPrefab` and `PlayerPrefab` empty for now — we'll assign them next.

### 4. Create Player and Enemy Prefabs

#### Player Prefab
1. Right-click Hierarchy → `3D Object > Cube`
2. Rename to `Player`
3. Drag `DemoInputRouter` component onto it (from Scripts/Core)
4. Adjust scale: X=1, Y=0.5, Z=1
5. Add a `Rigidbody` component → Check `Use Gravity` → Set `Drag` to 5
6. Add a `Capsule Collider` → Set Height=0.5, Radius=0.3
7. In Inspector, set `Tag` to `Player`
8. Drag `Player` from Hierarchy into `Assets/Prefabs` folder → name it `PlayerPrefab`
9. Delete `Player` from Hierarchy

#### Enemy Prefab
1. Right-click Hierarchy → `3D Object > Cube`
2. Rename to `Enemy`
3. Remove `DemoInputRouter` (not needed)
4. Adjust scale: X=0.8, Y=0.4, Z=0.8
5. Add `Rigidbody` → Check `Use Gravity`
6. Add `Capsule Collider` → Height=0.4, Radius=0.25
7. Set `Tag` to `Enemy`
8. Change Material color to Red (optional)
9. Drag `Enemy` into `Assets/Prefabs` → name it `EnemyPrefab`
10. Delete `Enemy` from Hierarchy

### 5. Assign Prefabs to SpawnManager
1. Select `GameMaster`
2. In `SimpleSpawnManager` component:
   - Drag `PlayerPrefab` from `Assets/Prefabs` into `PlayerPrefab` slot
   - Drag `EnemyPrefab` from `Assets/Prefabs` into `EnemyPrefab` slot

### 6. Set Up Lighting and Camera
1. Delete default `Main Camera`
2. Right-click Hierarchy → `Camera` → Rename to `MainCamera`
3. Position: X=0, Y=5, Z=-10
4. Rotation: X=30, Y=0, Z=0
5. Set `Clear Flags` to `Skybox`
6. Add `Directional Light` (right-click → `Light > Directional Light`)
7. Rotate to cast shadows downward (X=315, Y=45, Z=0)

### 7. Test the Scene
1. Press Play
2. You should see:
   - One player cube at the first spawn point
   - Four enemy cubes at the remaining spawn points
   - Player responds to WASD/arrow keys
3. The `GameLoopManager` should log `Game started.` when Play is pressed

## Next Steps
- Attach AI behavior to enemies
- Add UI for score/health
- Add level boundaries

> ✅ This is now a runnable local demo. No editor automation required.
