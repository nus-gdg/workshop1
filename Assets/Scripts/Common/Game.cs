using UnityEngine;

namespace Common
{
    public class Game : MonoBehaviour
    {
        private static Game _instance;
        public static Game Instance => _instance;
        
        [SerializeField]
        private new AudioManager audio;
        public AudioManager Audio => audio;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Load()
        {
            _instance = Instantiate(Resources.Load<Game>("Game"));
            DontDestroyOnLoad(_instance);
        }
    }
}
