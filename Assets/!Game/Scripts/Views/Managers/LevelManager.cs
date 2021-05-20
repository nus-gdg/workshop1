using System.Collections;
using System.Collections.Generic;
using Project.Models.Levels;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.Views.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private Level CurrentLevel;
        private Level CurrentLoadingLevel;

        private List<Level> UnloadingLevels = new List<Level>();

        void Start()
        {
            // Scene scene = SceneManager.GetActiveScene();
            CurrentLevel = ScriptableObject.CreateInstance<Level>();
#if UNITY_EDITOR
            CurrentLevel.EditorOnly_SceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(UnitySceneManager.GetActiveScene().path);
            EditorOnly_LevelHelper.RefreshBuildSettings();
#endif
        }

        // loads a level and unspawns current level
        public bool RequestLoadLevel(Level level)
        {
            if (level == null)
            {
                Debug.LogError("LevelManager.RequestLoadLevel tried to load null level. Failing");
                return false;
            }

            // validation
            if (CurrentLevel == level)
            {
                Debug.Log("LevelManager.RequestLoadLevel tried to load current level. Treating request as success");
                return true;
            }

            if (level == CurrentLoadingLevel)
            {
                Debug.Log("LevelManager.RequestLoadLevel tried to load current level. Treating request as success");
                return true;
            }

            if (UnloadingLevels.Contains(level))
            {
                Debug.Log("LevelManager.RequestLoadLevel tried to load an unloading level. Something went wrong");
                return false;
            }
#if UNITY_EDITOR
            string scenePath = AssetDatabase.GetAssetPath(level.EditorOnly_SceneAsset);
            Scene scene = UnitySceneManager.GetSceneByPath(scenePath);
            if (scene.isLoaded)
            {
                Debug.Log("LevelManager.RequestLoadLevel scene is loaded but not recorded as a level. Did you try to load a scene while its already open? Something went wrong");
                return false;
            }
#endif

            StartCoroutine(LoadLevelAsync(level));
            return true;
        }

        IEnumerator LoadLevelAsync(Level level)
        {
            Assert.IsNull(CurrentLoadingLevel);
            CurrentLoadingLevel = level;
            AsyncOperation asyncOp = UnitySceneManager.LoadSceneAsync(level.BuildIndex, LoadSceneMode.Additive);
            while (!asyncOp.isDone)
            {
                yield return null;
            }
            CurrentLoadingLevel = null;
            UpdateCurrentLevel(level);
        }

        IEnumerator UnloadLevelAsync(Level level)
        {
            UnloadingLevels.Add(level);
            AsyncOperation asyncOp = UnitySceneManager.UnloadSceneAsync(level.BuildIndex);
            while (!asyncOp.isDone)
            {
                yield return null;
            }
            UnloadingLevels.Remove(level);
        }

        void UpdateCurrentLevel(Level level)
        {
            if (CurrentLevel != null)
                StartCoroutine(UnloadLevelAsync(CurrentLevel));

            CurrentLevel = level;
        }
    }

}
