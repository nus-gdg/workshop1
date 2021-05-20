using Project.Common;
using Project.Controllers;
using Project.Data;
using Project.Models;
using Project.Views;
using UnityEngine;

namespace Project
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        [SerializeField] private GameObject uiContainer;
        [SerializeField] private GameObject logicContainer;
        [SerializeField] private GameObject modelContainer;
        [SerializeField] private GameObject storageContainer;
        [SerializeField] private GameObject configContainer;

        [SerializeField] private string configFilePath = "config.json";

        public IUi Ui { get; private set; }
        public ILogic Logic { get; private set; }
        public IModel Model { get; private set; }
        public IStorage Storage { get; private set; }
        public Config Config { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Load()
        {
            Instance = Instantiate(Resources.Load<Game>("Game"));
            DontDestroyOnLoad(Instance);
        }

        private void Awake()
        {
            InitConfig();
            InitStorage(Config);
            InitModel(Storage);
            InitLogic(Model, Storage);
            InitUi(Logic);
        }

        private void InitConfig()
        {
            Config = configContainer.GetComponent<Config>();
            Config.Init(GetAssetFilePath(configFilePath));
        }

        private void InitStorage(Config config)
        {
            Storage = storageContainer.GetComponent<IStorage>();

            string saveDataFilePath = GetAssetFilePath(config.SaveDataFilePath);
            string userPrefsFilePath = GetAssetFilePath(config.UserPrefsFilePath);

            Storage.Init(saveDataFilePath, userPrefsFilePath);
        }

        private void InitModel(IStorage storage)
        {
            Model = modelContainer.GetComponent<IModel>();

            SaveData saveData = storage.ReadSaveData();
            UserPrefs userPrefs = storage.ReadUserPrefs();

            Model.Init(saveData, userPrefs);
        }

        private void InitLogic(IModel model, IStorage storage)
        {
            Logic = logicContainer.GetComponent<ILogic>();
            Logic.Init(model, storage);
        }

        private void InitUi(ILogic logic)
        {
            Ui = uiContainer.GetComponent<IUi>();
            Ui.Init(logic);
        }

        private string GetAssetFilePath(string filePath)
        {
            return $"{Application.dataPath}/{filePath}";
        }
    }
}
