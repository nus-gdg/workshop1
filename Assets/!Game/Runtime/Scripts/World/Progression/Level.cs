using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Progression
{

    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Progression/Level")]
    public class Level : ScriptableObject
    {
        public string ScenePath;
    }

    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        int _choiceIndex = 0;
        int _prevSceneCount = 0;

        void HandleSceneCountChange(string[] scenePaths)
        {
            Level level = target as Level;

            int index = System.Array.FindIndex(scenePaths, scene => scene == level.ScenePath);
            if (index != -1)
            {
                _choiceIndex = index;
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            DrawDefaultInspector();
            EditorGUI.EndDisabledGroup();

            string[] scenePaths = new string[SceneManager.sceneCountInBuildSettings];
            string[] unformattedScenePaths = new string[SceneManager.sceneCountInBuildSettings];

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
            {
                scenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i);
                unformattedScenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i).Replace("/", "\\"); // hack to remove path selection dropdown menu...
            }

            if (_prevSceneCount != SceneManager.sceneCountInBuildSettings)
            {
                HandleSceneCountChange(scenePaths);
                _prevSceneCount = SceneManager.sceneCountInBuildSettings;
            }

            _choiceIndex = EditorGUILayout.Popup("Scene Path Picker", _choiceIndex, unformattedScenePaths);
            var Level = target as Level;
            if (SceneManager.sceneCountInBuildSettings > 0)
            {
                Level.ScenePath = scenePaths[_choiceIndex];
            }
            else
            {
                Level.ScenePath = "NO SCENES IN BUILD SETTINGS, PLEASE ADD TO MAKE LEVEL LOADABLE!";
            }
            EditorUtility.SetDirty(target);
            EditorGUILayout.LabelField("Dont see your scene here? Add it to the build settings: File > Build Settings > Add Open Scenes");
        }
    }

}
