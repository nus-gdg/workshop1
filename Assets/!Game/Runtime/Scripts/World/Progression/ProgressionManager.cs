using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

namespace Progression
{

    public class ProgressionManager : MonoBehaviour
    {
        private Level CurrentLevel;
        private Level CurrentLoadingLevel;

        private List<Level> UnloadingLevels = new List<Level>();

        void Start()
        {
            Scene scene = SceneManager.GetActiveScene();
            CurrentLevel = ScriptableObject.CreateInstance<Level>();
            CurrentLevel.ScenePath = scene.path;
        }

        // loads a level and unspawns current level
        public bool RequestLoadLevel(Level level)
        {
            if (level == null)
            {
                Debug.LogError("ProgressionManager.RequestLoadLevel tried to load null level. Failing");
                return false;
            }

            // validation
            if (CurrentLevel == level)
            {
                Debug.Log("ProgressionManager.RequestLoadLevel tried to load current level. Treating request as success");
                return true;
            }

            if (level == CurrentLoadingLevel)
            {
                Debug.Log("ProgressionManager.RequestLoadLevel tried to load current level. Treating request as success");
                return true;
            }

            if (UnloadingLevels.Contains(level))
            {
                Debug.Log("ProgressionManager.RequestLoadLevel tried to load an unloading level. Something went wrong");
                return false;
            }

            Scene scene = SceneManager.GetSceneByPath(level.ScenePath);
            if (scene.isLoaded)
            {
                Debug.Log("ProgressionManager.RequestLoadLevel scene is loaded but not recorded as a level. Did you try to load a scene while its already open? Something went wrong");
                return false;
            }

            StartCoroutine(LoadLevelAsync(level));
            return true;
        }

        IEnumerator LoadLevelAsync(Level level)
        {
            Assert.IsNull(CurrentLoadingLevel);
            CurrentLoadingLevel = level;
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(level.ScenePath, LoadSceneMode.Additive);
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
            AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(level.ScenePath);
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
