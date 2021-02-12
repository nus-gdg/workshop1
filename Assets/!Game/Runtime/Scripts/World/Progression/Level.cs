using System.Collections;
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
        public string ScenePath;
#if UNITY_EDITOR
        public SceneAsset SceneAsset;
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
            EditorGUILayout.LabelField("Scene Path", level.ScenePath);
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginChangeCheck();
            SceneAsset newScene = EditorGUILayout.ObjectField("Scene Asset", level.SceneAsset, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {

                string newPath = AssetDatabase.GetAssetPath(newScene);
                level.ScenePath = newPath;
                level.SceneAsset = newScene;
                EditorUtility.SetDirty(target);
            }
        }
    }
#endif

}
