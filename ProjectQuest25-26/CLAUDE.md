# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a **Unity 6000.3.10f1** project using the **Universal Render Pipeline (URP)**. The project includes a basic 3D scene setup with modern Unity features and is configured for cross-platform development with Linux build tools included.

**Key Technologies:**
- Unity Engine 6000.3.10f1
- Universal Render Pipeline (URP)
- New Input System (com.unity.inputsystem)
- Visual Scripting support
- Timeline for animation/cinematic work
- AI Navigation package for NPC pathfinding
- Test Framework for unit testing

## Project Structure

```
Assets/                    # Game content, scripts, scenes, and art
├── Scenes/               # Unity scene files
├── Scripts/              # C# game logic (organized by subsystem)
├── Settings/             # URP rendering profiles and configurations
├── TutorialInfo/         # Sample tutorial setup (can be removed)
└── ...

ProjectSettings/          # Unity project configuration
Packages/                 # Package dependencies (manifest.json, packages-lock.json)
Library/                  # Unity internal cache (gitignored)
Logs/                     # Build and editor logs (gitignored)
```

## Common Development Commands

### Opening the Project

```bash
# Open the project in the Unity Editor
unity -projectPath . &

# Or use the Unity Hub to open the project
```

### Building

```bash
# Build for development (PC/Linux)
# Use the Unity Editor: File > Build Settings > Build

# Standalone builds are located in the Builds/ directory after compilation
```

### Running Tests

```bash
# Run tests via the Unity Editor
# Window > Testing > Test Runner
# Or run from command line:
unity -projectPath . -runTests -testPlatform editmode -logFile -
unity -projectPath . -runTests -testPlatform playmode -logFile -
```

### Scene Management

- **Main Scene**: `Assets/Scenes/SampleScene.unity`
- Add new scenes to `ProjectSettings/EditorBuildSettings.asset` for inclusion in builds

## Architecture Notes

### Rendering Pipeline

The project uses **Universal Render Pipeline (URP)** configured with:
- **PC Renderer**: High-quality settings for desktop platforms (`Assets/Settings/PC_RPAsset.asset`)
- **Mobile Renderer**: Optimized settings for mobile devices (`Assets/Settings/Mobile_Renderer.asset`)

Switch renderers in `ProjectSettings/QualitySettings.asset` or via code using `QualitySettings.SetQualityLevel()`.

### Input System

The project uses the **New Input System** (com.unity.inputsystem 1.18.0):
- Input actions defined in `Assets/InputSystem_Actions.inputactions`
- Create custom input maps in the Input Actions editor (double-click .inputactions file)
- Reference actions in scripts via `InputAction` or the generated code

### Script Organization

When adding new scripts to `Assets/Scripts/`, organize by feature/system:
```
Assets/Scripts/
├── Player/           # Player character logic
├── Enemies/          # NPC and enemy behavior
├── UI/              # UI scripts and managers
├── Managers/        # Game managers (GameManager, AudioManager, etc.)
├── Utilities/       # Reusable helper functions
└── Editor/          # Editor-only tools and inspectors
```

## Important Files & Configuration

- **ProjectSettings/ProjectSettings.asset**: Core project settings (resolution, quality levels, platforms)
- **ProjectSettings/URPProjectSettings.asset**: URP-specific global settings
- **Packages/manifest.json**: Declared package dependencies (edit to add/remove packages)
- **Packages/packages-lock.json**: Locked package versions (auto-generated, don't edit directly)

## Testing

The project includes **com.unity.test-framework 1.6.0**:

1. Create test scripts in `Assets/Tests/` (create this folder if needed)
2. Test classes should inherit from `UnityTest` or use the `[Test]` attribute
3. Use the Test Runner window to run and debug tests
4. Tests run in both Edit Mode and Play Mode

Example test structure:
```csharp
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MyGameTests
{
    [Test]
    public void TestGameLogic()
    {
        // Test code here
        Assert.IsTrue(true);
    }
}
```

## Git Workflow

- **Main branch**: Default development branch
- Recent commits include project initialization with corrected .gitignore
- Standard Unity gitignore is in place (Library/, Temp/, Logs/, etc. are excluded)

**When committing:**
- Avoid committing `Library/`, `Temp/`, `Logs/`, `UserSettings/` directories
- Include `ProjectSettings/`, `Assets/`, `Packages/manifest.json`, and `Packages/packages-lock.json`
- Use meaningful commit messages for scene/prefab changes and script additions

## IDE & Editor Integration

The project is configured for both **Rider** and **Visual Studio**:
- Open scripts with IDE via double-click in Project window
- IDE integration is handled by `com.unity.ide.rider` and `com.unity.ide.visualstudio`
- The project generates `.csproj` and `.sln` files (gitignored, regenerated on load)

## Performance & Quality Settings

The project includes multiple quality levels in `ProjectSettings/QualitySettings.asset`:
- Adjust draw call counts, shadow settings, and LOD bias per quality level
- Use `QualitySettings.GetQualityLevel()` / `SetQualityLevel()` to change quality at runtime

## Debugging Tips

- Use the **Console** window for runtime logs
- **Debug.Log()**, **Debug.LogWarning()**, **Debug.LogError()** for output
- The Profiler (Window > Analysis > Profiler) shows CPU, GPU, memory usage
- Use **Play Mode Debug** in the Inspector to inspect objects while the game runs