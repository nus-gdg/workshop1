using Core.Managers;
using UnityEngine;

namespace Core
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
        private StoryManager story;
        public StoryManager Story => story;

        [SerializeField]
        private Progression.LevelManager levels;
        public Progression.LevelManager Levels => levels;

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
            _instance._input.Player.Enable();

            DontDestroyOnLoad(_instance);
        }
    }
}
