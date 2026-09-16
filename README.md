# Chronovortex

**2D Platformer with Time Travel Mechanics**

Diploma project — a 2D platformer featuring time travel and mode-switching systems.

---

## Overview

**Chronovortex** is a 2D platformer where the player can switch between different time modes and teleport between linked space-time points.

The project focuses on clean architecture and modular design, including:
- Player State Machine
- Interface-based separation of concerns
- Mode switching system
- Paired-point teleportation system

---

## Core Systems

### 1. Mode System (`ModeActivator` + `ModeHandler`)

System for switching between two modes (e.g. past / future).

- Activates / deactivates corresponding GameObjects
- Changes UI element transparency
- Controls: `1` / `2`

### 2. Time Teleport System (`PlayerTeleportSystem`)

Teleportation mechanics between paired points (time portals).

- Automatically finds the nearest point
- Teleports the player to the linked pair
- Supports different access levels (`Only1`, `Only2`, `Only3`, `All`)
- Visualizes teleport pairs in the editor (Gizmos)

### 3. Player Controller

Full player control system:

- Movement + Jump
- Lives / Health system
- Smart respawn (searches for safe position)
- State Machine (`Idle` → `Walk` → `Jump`)
- Walk and jump animations

---

## Architecture

The project emphasizes clean code and separation of concerns:

| Module                  | Responsibility                      |
|-------------------------|-------------------------------------|
| `PlayerController`      | Main orchestrator                   |
| `PlayerStateMachine`    | Player state management             |
| `PlayerAnimation`       | Sprite animation                    |
| `GroundChecker`         | Ground detection                    |
| `HealthService`         | Lives system                        |
| `ModeActivator`         | Mode switching                      |
| `PlayerTeleportSystem`  | Time-point teleportation            |

Interfaces used:
- `IPlayerMovement`
- `IPlayerJump`
- `IHealthService`
- `IPlayerState`
- `IModeSwitcher`
- `IModeHandler`

---

## Controls

| Key           | Action                              |
|---------------|-------------------------------------|
| `A` / `D`     | Move left / right                   |
| `Space`       | Jump                                |
| `1` / `2`     | Switch time modes                   |
| `1` / `2` / `3` | Teleport (when available)         |

---

## Technical Stack

- **Engine:** Unity (2D)
- **Language:** C#
- **Architecture:** State Machine + Interface-based design
- **Physics:** Rigidbody2D + OverlapCircle

---

## Project Structure (Main Scripts)
