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

        public UnityEvent OnPauseEvent;
        public UnityEvent OnUnpauseEvent;

        [SerializeField]
        private CameraManager cameraManager;
        public CameraManager CameraManager => cameraManager;

        // TODO: Add system to pause entities
        void OnApplicationQuit()
        {
            Player = null;
            Cursor = null;
        }

        public void PauseWorld()
        {
            Time.timeScale = 0.0f;
            OnPauseEvent.Invoke();
        }

        public void UnpauseWorld()
        {
            Time.timeScale = 1.0f;
            OnUnpauseEvent.Invoke();
        }

        public delegate void OnSceneLoadedFunc(Scene scene);
        public delegate void OnSceneLoadFailFunc();

        public void LoadScene(string name)
        {
            LoadScene(name, (scene) => { }, () => { });
        }

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

        public void UnloadScene(string name)
        {
            StartCoroutine(UnloadSceneAsyncOperation(name, () => { }));
        }
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
