using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Levels
{

    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Progression/Level")]
    public class Level : ScriptableObject
    {
        public int BuildIndex;
#if UNITY_EDITOR
        public SceneAsset EditorOnly_SceneAsset;
        public bool EditorOnly_IsStartLevel = false;
        void OnDestroy()
        {
            EditorOnly_SceneAsset = null;
            EditorOnly_LevelHelper.RefreshBuildSettings();
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Level level = target as Level;
            string scenePath = AssetDatabase.GetAssetPath(level.EditorOnly_SceneAsset);

            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUILayout.IntField("Build Index", level.BuildIndex);
                EditorGUILayout.LabelField("Scene Path", scenePath);
                EditorGUILayout.LabelField("Current Start Level ", SceneUtility.GetScenePathByBuildIndex(0));
                EditorGUILayout.LabelField("Debug - Editor Only Start Level ", level.EditorOnly_IsStartLevel ? "Is Start Level" : "Not Start Level");
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginChangeCheck();
            SceneAsset newScene = EditorGUILayout.ObjectField("Scene Asset", level.EditorOnly_SceneAsset, typeof(SceneAsset), false) as SceneAsset;
            if (EditorGUI.EndChangeCheck())
            {
                string newPath = AssetDatabase.GetAssetPath(newScene);
                level.EditorOnly_SceneAsset = newScene;
                EditorUtility.SetDirty(target);
                EditorOnly_LevelHelper.RemoveAsStartLevel(level);
                EditorOnly_LevelHelper.RefreshBuildSettings();
            }

            string startScenePath = SceneUtility.GetScenePathByBuildIndex(0);
            if (level.EditorOnly_SceneAsset != null)
            {
                if (startScenePath == scenePath)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    GUILayout.Button("Already the Start Level");
                    EditorGUI.EndDisabledGroup();
                }
                else
                {
                    if (GUILayout.Button("Set As Start Level"))
                    {
                        EditorOnly_LevelHelper.SetStartLevel(level);
                        EditorOnly_LevelHelper.RefreshBuildSettings();
                    }
                }
            }

            if (GUILayout.Button("Force Refresh Build Settings"))
            {
                EditorOnly_LevelHelper.RefreshBuildSettings();
            }
        }
    }

    public static class EditorOnly_LevelHelper
    {
        public static List<Level> LoadAllLevels()
        {
            List<Level> levels = new List<Level>();
            string[] assetNames = AssetDatabase.FindAssets("t:Level");
            foreach (string SOName in assetNames)
            {
                string SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                Level level = AssetDatabase.LoadAssetAtPath<Level>(SOpath);
                levels.Add(level);
            }
            return levels;
        }

        public static Level GetStartLevel()
        {
            List<Level> levels = LoadAllLevels();
            foreach (Level level in levels)
            {
                if (level.EditorOnly_IsStartLevel)
                    return level;
            }
            return null;
        }

        public static Level InferStartLevelFromCurrentBuildSettings()
        {
            string startScenePath = SceneUtility.GetScenePathByBuildIndex(0);
            List<Level> levels = LoadAllLevels();
            foreach (Level level in levels)
            {
                string scenePath = AssetDatabase.GetAssetPath(level.EditorOnly_SceneAsset);
                if (scenePath == startScenePath)
                    return level;
            }
            return null;
        }

        public static void RemoveAsStartLevel(Level level)
        {
            level.EditorOnly_IsStartLevel = false;
            Level currentStartLevel = GetStartLevel();

            if (currentStartLevel == null)
            {
                currentStartLevel = InferStartLevelFromCurrentBuildSettings();
                if (currentStartLevel == null) // still null
                {
                    return;
                }
            }

            SetStartLevel(currentStartLevel);
        }

        public static void SetStartLevel(Level startLevel)
        {
            string startLevelScenePath = AssetDatabase.GetAssetPath(startLevel.EditorOnly_SceneAsset);
            List<Level> levels = LoadAllLevels();
            foreach (Level level in levels)
            {
                string scenePath = AssetDatabase.GetAssetPath(level.EditorOnly_SceneAsset);
                level.EditorOnly_IsStartLevel = scenePath == startLevelScenePath;
            }
        }

        public static void RefreshBuildSettings()
        {
            List<Level> levels = LoadAllLevels();
            levels.Sort((level1, level2) =>
            {
                if (level1.EditorOnly_IsStartLevel)
                    return -1;

                if (level2.EditorOnly_IsStartLevel)
                    return 1;

                string scenePath = AssetDatabase.GetAssetPath(level1.EditorOnly_SceneAsset);
                string scenePath2 = AssetDatabase.GetAssetPath(level2.EditorOnly_SceneAsset);

                return scenePath.CompareTo(scenePath2);
            });

            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            foreach (Level level in levels)
            {
                string scenePath = AssetDatabase.GetAssetPath(level.EditorOnly_SceneAsset);
                if (string.IsNullOrEmpty(scenePath))
                {
                    level.BuildIndex = -1;
                }
                else
                {
                    int index = editorBuildSettingsScenes.FindIndex(setting => setting.path == scenePath);
                    if (index == -1)
                    {
                        level.BuildIndex = editorBuildSettingsScenes.Count;
                        editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                    }
                    else
                    {
                        level.BuildIndex = index;
                    }
                }

                level.EditorOnly_IsStartLevel = level.BuildIndex == 0;
            }


            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
        }

    }
#endif

}
