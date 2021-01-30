using UnityEngine;

namespace Common
{
    public class Game : MonoBehaviour
    {
        private static Game _instance;
        public static Game Instance => _instance;
        
        [SerializeField]
        private WorldManager world;
        public WorldManager World => world;
        
        [SerializeField]
        private new AudioManager audio;
        public AudioManager Audio => audio;
        
        [SerializeField]
        private DialogueManager dialogue;
        public DialogueManager Dialogue => dialogue;
        
        [SerializeField]
        private PoolManager pool;
        public PoolManager Pool => pool;
        
        private InputManager _input;
        public InputManager Input => _input;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Load()
        {
            _instance = Instantiate(Resources.Load<Game>("Game"));
            _instance._input = new InputManager();
            DontDestroyOnLoad(_instance);
        }
    }
}
