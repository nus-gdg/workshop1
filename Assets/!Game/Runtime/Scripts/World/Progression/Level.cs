using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine.SceneManagement;

namespace Progression
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

            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUILayout.IntField("Build Index", level.BuildIndex);

                string path = AssetDatabase.GetAssetPath(level.EditorOnly_SceneAsset);
                EditorGUILayout.LabelField("Scene Path", path);

                Level startLevel = EditorOnly_LevelHelper.GetStartLevel();
                EditorGUILayout.LabelField("Current Start Level ", startLevel != null ? AssetDatabase.GetAssetPath(startLevel.EditorOnly_SceneAsset) : "NONE");
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginChangeCheck();
            SceneAsset newScene = EditorGUILayout.ObjectField("Scene Asset", level.EditorOnly_SceneAsset, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                string newPath = AssetDatabase.GetAssetPath(newScene);
                level.EditorOnly_SceneAsset = newScene;
                EditorUtility.SetDirty(target);
                EditorOnly_LevelHelper.RefreshBuildSettings();
                EditorOnly_LevelHelper.RefreshStartLevelFlags();
            }

            if (level.EditorOnly_IsStartLevel)
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

            if (GUILayout.Button("Force Refresh Build Settings"))
            {
                EditorOnly_LevelHelper.RefreshBuildSettings();
            }
        }
    }

    public static class EditorOnly_LevelHelper
    {
        public static Level GetStartLevel()
        {
            Level[] levels = Resources.FindObjectsOfTypeAll<Level>();
            foreach (Level level in levels)
            {
                if (level.EditorOnly_IsStartLevel)
                    return level;
            }
            return null;
        }

        public static void RefreshStartLevelFlags()
        {
            Level level = GetStartLevel();
            if (level == null)
                return;

            SetStartLevel(level);
        }

        public static void SetStartLevel(Level startLevel)
        {
            string startLevelScenePath = AssetDatabase.GetAssetPath(startLevel.EditorOnly_SceneAsset);
            Level[] levels = Resources.FindObjectsOfTypeAll<Level>();
            foreach (Level level in levels)
            {
                string scenePath = AssetDatabase.GetAssetPath(level.EditorOnly_SceneAsset);
                level.EditorOnly_IsStartLevel = scenePath == startLevelScenePath;
            }
        }

        public static void RefreshBuildSettings()
        {
            Level[] levels = Resources.FindObjectsOfTypeAll<Level>();
            System.Array.Sort(levels, (level1, level2) =>
            {
                if (level1.EditorOnly_IsStartLevel)
                    return -1;
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
                    continue;
                }

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


            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
        }

    }
#endif

}
