using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using ProjectQuest.Levels;

/// <summary>
/// Editor script to set up the SampleScene with TilemapGenerator and camera positioning.
/// </summary>
public static class SceneSetup
{
    [MenuItem("ProjectQuest/Setup Ice Age Level")]
    public static void SetupIceAgeLevel()
    {
        // Load SampleScene if not already loaded
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity", OpenSceneMode.Single);

        if (!scene.isLoaded)
        {
            Debug.LogError("Failed to load SampleScene.unity");
            return;
        }

        // Find or create TilemapGenerator GameObject
        GameObject tilemapGenGO = GameObject.Find("TilemapGenerator");
        if (tilemapGenGO == null)
        {
            tilemapGenGO = new GameObject("TilemapGenerator");
            EditorSceneManager.MarkSceneDirty(scene);
        }

        // Ensure the TilemapGenerator component is attached
        TilemapGenerator tilemapGen = tilemapGenGO.GetComponent<TilemapGenerator>();
        if (tilemapGen == null)
        {
            tilemapGen = tilemapGenGO.AddComponent<TilemapGenerator>();
        }

        // Wire up prefab references
        GameObject snowTilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/snow_tile.prefab");
        GameObject iceTilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/ice_tile.prefab");

        if (snowTilePrefab == null || iceTilePrefab == null)
        {
            Debug.LogError("Could not find snow_tile.prefab or ice_tile.prefab in Assets/Prefabs/");
            return;
        }

        // Set the serialized fields via reflection (since they're private)
        var snowField = tilemapGen.GetType().GetField("snowTilePrefab",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var iceField = tilemapGen.GetType().GetField("iceTilePrefab",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (snowField != null)
            snowField.SetValue(tilemapGen, snowTilePrefab);
        if (iceField != null)
            iceField.SetValue(tilemapGen, iceTilePrefab);

        EditorUtility.SetDirty(tilemapGen);

        // Position camera for 2.5D side-scrolling view
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(125f, 30f, -20f);
            mainCamera.transform.LookAt(new Vector3(125f, 0f, 100f));
            EditorSceneManager.MarkSceneDirty(scene);
        }

        EditorSceneManager.SaveScene(scene);
        Debug.Log("Ice Age Level setup complete! Ready to generate tilemap via context menu.");
    }
}
