# COSE Project - Developer Manual

## Overview

The COSE Project is a Unity-based interactive experience structured around modular scenes, each with localized text triggers, 3D models, and coin collection mechanics. Its architecture is built to support scalable interactions through shared base scripts, clean separation of logic, and a central persistence system for cross-scene memory.

## Core System Architecture

### 1. Localization-Driven Interaction

All user-facing text is stored using Unity Localization Tables. Text triggers use localization keys, not raw strings, allowing for flexible multi-language support.

- Scripts like `TextInteraction.cs` (and its subclass scripts) dynamically fetch and render localized content from a key.
- Text appears on user interaction (e.g., collision, click), then optionally disappears.

### 2. Event-Based Scene Interaction

The system uses C# events to decouple triggers from UI responses and scene-specific reactions:

| Trigger Event | Listener | Purpose |
|---------------|----------|---------|
| `SphereTrigger.OnSphereTriggered` | `TextInteraction`, `IngeInteractionManager` | Triggers sphere-based interactions |
| `CoinTrigger.OnCoinTriggered` | `TextInteraction`, `PersistenceManager` | Collectible logic and localized feedback |
| `LayerInteraction.OnLayerClicked` | `TextInteraction`, `IngeInteractionManager`, `GroupLayerInteraction` | Layer-specific interactions |
| `GroupLayerInteraction.OnLayerGroupClicked` | `LayerInteraction`, `IngeInteractionManager` | Grouped behavior for complex scenes |

### 3. Object Highlighting

All clickable objects feature an Outline component:

- Disabled on start
- Enabled on `OnMouseDown()`
- Automatically disabled after a timeout or when clicking another object

Managed by `LayerInteraction.cs` and `GroupLayerInteraction.cs`.

### 4. Coin Collection System

This system supports:

- One-time triggers (each coin deactivates after collection)
- Persistent memory of collected coins across scenes
- Dynamic updates to the UI

Key script: `PersistenceManager.cs`

Features:

- Uses `HashSet<string>` for efficient tracking
- Reactivates coins in the outro scene
- Activates a "Final Room" once all scenes are visited

### 5. Base Script Inheritance

Scene-specific logic extends base behaviors:

- `TextInteraction` is extended by each scene's subclass script for contextual text logic
- Each scene defines its own `InteractionManager` to control animations, model logic, and UI based on events

### 6. Scene and Flow Structure

Scenes are structured with consistent tagging and naming:

| Tag | Usage |
|-----|-------|
| `Player` | Used in trigger detection |
| `CoinCounterText` | Displays coin count |
| `FinalRoom` | Hidden until all rooms visited |
| `SCENE_*` | Standardized scene names used in `PersistenceManager` |

Scenes progress linearly, with gated access to final content based on visited scenes and coin collection.

## How to Extend or Modify

### Add New Interactables

1. Create a new GameObject (e.g., coin or layer)
2. Add the appropriate trigger script (`CoinTrigger`, `LayerInteraction`, etc.)
3. Assign a localization key in the inspector
4. Register interactions in your scene's `InteractionManager` if needed

### Add New Scenes

1. Name it using the `SCENE_NAME_HYPOTHESIS` format
2. Add appropriate tags and components to objects
3. Include the scene in Build Settings
4. `PersistenceManager` will auto-track coins and visits

### Add New Localized Text

1. Open Unity's Localization Tables
2. Add a new key-value entry
3. Use that key in scene scripts or text references

### Reset Progress

Use the following methods from `PersistenceManager`:

```csharp
ResetCoins();
ResetVisitedRooms();
```

These can be hooked to UI buttons or debug tools for development.

## Design Patterns in Use

- Singleton pattern for global systems like `PersistenceManager`
- Event-driven architecture for scene interactions
- String-key localization for all displayed text

## Known Limitations

- Coins and triggers must have unique text keys; no internal ID system
- Outline component usage is not optimized for large-scale object counts
- Final Room logic assumes 4 scenes; changing this requires modifying `IsRoomScene()`

## Notes

This documentation outlines the functional foundation for working with the project. Scene-specific logic should extend this base framework using the same architectural conventions.

For detailed per-scene interaction logic, refer to that scene's `InteractionManager` implementation.
