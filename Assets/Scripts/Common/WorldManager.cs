using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Common
{
    public class WorldManager : MonoBehaviour
    {
        public Entity.Player Player { get; set; }
        public Entity.Cursor Cursor { get; set; }

        /// <summary>
        /// Callback when world is paused
        /// </summary>
        public UnityEvent OnPauseEvent;

        /// <summary>
        /// Callback when world is unpaused
        /// </summary>
        public UnityEvent OnUnpauseEvent;

        [SerializeField]
        private CameraManager cameraManager;
        public CameraManager CameraManager => cameraManager;

        void OnApplicationQuit()
        {
            Player = null;
            Cursor = null;
        }

        /// <summary>
        /// Pauses world
        /// </summary>
        public void PauseWorld()
        {
            Time.timeScale = 0.0f;
            OnPauseEvent.Invoke();
        }

        /// <summary>
        /// Unpauses world
        /// </summary>
        public void UnpauseWorld()
        {
            Time.timeScale = 1.0f;
            OnUnpauseEvent.Invoke();
        }

        public delegate void OnSceneLoadedFunc(Scene scene);
        public delegate void OnSceneLoadFailFunc();

        /// <summary>
        /// Loads scene via name
        /// </summary>
        /// <param name="name">Name of scene. Expects scene to be regesitered in build settings</param>
        public void LoadScene(string name)
        {
            LoadScene(name, (scene) => { }, () => { });
        }

        /// <summary>
        /// Loads scene via name
        /// </summary>
        /// <param name="name">Name of scene. Expects scene to be regesitered in build settings</param>
        /// <param name="successFunc">Callback when load succeeds</param>
        /// <param name="failFunc">Callback when load fails</param>
        public void LoadScene(string name, OnSceneLoadedFunc successFunc, OnSceneLoadFailFunc failFunc)
        {
            Scene scene = SceneManager.GetSceneByName(name);
            if (scene.isLoaded)
            {
                // if scene is already loaded, just return success
                // we don't want duplicate scenes
                successFunc(scene);
                return;
            }
            StartCoroutine(LoadSceneAsyncOperation(name, successFunc, failFunc));
        }

        public delegate void OnSceneUnloaded();

        /// <summary>
        /// Unload scene via name
        /// </summary>
        /// <param name="name">Name of scene. Expects scene to be regesitered in build settings</param>
        public void UnloadScene(string name)
        {
            StartCoroutine(UnloadSceneAsyncOperation(name, () => { }));
        }

        /// <summary>
        /// Unload scene via name
        /// </summary>
        /// <param name="name">Name of scene. Expects scene to be regesitered in build settings</param>
        /// <param name="func">Callback when unload finishes</param>
        public void UnloadScene(string name, OnSceneUnloaded func)
        {
            StartCoroutine(UnloadSceneAsyncOperation(name, func));
        }

        IEnumerator LoadSceneAsyncOperation(string name, OnSceneLoadedFunc successFunc, OnSceneLoadFailFunc failFunc)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            Scene scene = SceneManager.GetSceneByName(name);
            if (!scene.IsValid() || !scene.isLoaded)
            {
                failFunc();
                yield break;
            }

            CameraManager.OnSceneLoadedByWorld(scene);
            successFunc(scene);
        }

        IEnumerator UnloadSceneAsyncOperation(string name, OnSceneUnloaded func)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(name);
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
            func();
        }

    }
}
