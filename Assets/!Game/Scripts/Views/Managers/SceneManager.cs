using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Project.Views.Managers
{
    public class SceneManager : MonoBehaviour
    {
        private static int _enterSceneAnimTrigger = Animator.StringToHash("EnterScene");
        private static int _exitSceneAnimTrigger = Animator.StringToHash("ExitScene");

        [SerializeField]
        private Animator animator;

        private AsyncOperation _loadSceneAsync;

        public bool IsLoadingScene => _loadSceneAsync != null;

        public void LoadScene(string sceneName, RuntimeAnimatorController transition)
        {
            if (IsLoadingScene)
            {
                Debug.LogWarning("Another scene is being loaded at the moment");
                return;
            }
            StartCoroutine(LoadSceneRoutine(sceneName, transition));
        }

        private IEnumerator LoadSceneRoutine(string sceneName, RuntimeAnimatorController transition)
        {
            _loadSceneAsync = UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if (_loadSceneAsync == null)
            {
                yield break;
            }
            _loadSceneAsync.allowSceneActivation = false;

            if (transition != null)
            {
                animator.runtimeAnimatorController = transition;
            }
            animator.SetTrigger(_exitSceneAnimTrigger);
            yield return null;

            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return null;
            }

            _loadSceneAsync.allowSceneActivation = true;
            while (!_loadSceneAsync.isDone)
            {
                yield return null;
            }

            UnitySceneManager.UnloadSceneAsync(UnitySceneManager.GetActiveScene().name);

            animator.SetTrigger(_enterSceneAnimTrigger);
            yield return null;

            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return null;
            }

            _loadSceneAsync = null;
        }
    }
}
