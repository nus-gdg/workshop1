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
#if UNITY_EDITOR
        public string SceneName;
#endif
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

            string[] sceneNames = new string[SceneManager.sceneCountInBuildSettings];
            string[] scenePaths = new string[SceneManager.sceneCountInBuildSettings];

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
            {
                scenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i);
                sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }

            if (_prevSceneCount != SceneManager.sceneCountInBuildSettings)
            {
                HandleSceneCountChange(scenePaths);
                _prevSceneCount = SceneManager.sceneCountInBuildSettings;
            }

            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, sceneNames);
            var Level = target as Level;
            if (SceneManager.sceneCountInBuildSettings > 0)
            {
                Level.SceneName = sceneNames[_choiceIndex];
                Level.ScenePath = scenePaths[_choiceIndex];
            }
            else
            {
                Level.SceneName = "NO SCENES IN BUILD SETTINGS, PLEASE ADD TO MAKE LEVEL LOADABLE!";
                Level.ScenePath = "";
            }
            EditorUtility.SetDirty(target);
        }
    }

}
