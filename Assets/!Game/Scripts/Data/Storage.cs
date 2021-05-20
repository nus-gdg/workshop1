using Project.Models;
using UnityEngine;

namespace Project.Data
{
    public class Storage : MonoBehaviour, IStorage
    {
        [SerializeField]
        private SaveDataStorage saveDataStorage;

        [SerializeField]
        private UserPrefsStorage userPrefsStorage;

        public void Init(string saveDataFilePath, string userPrefsFilePath)
        {
            saveDataStorage.FilePath = saveDataFilePath;
            userPrefsStorage.FilePath = userPrefsFilePath;
        }

        public SaveData ReadSaveData()
        {
            return saveDataStorage.ReadData();
        }

        public UserPrefs ReadUserPrefs()
        {
            return userPrefsStorage.ReadData();
        }

        public void WriteSaveData(SaveData saveData)
        {
            saveDataStorage.WriteData(saveData);
        }

        public void WriteUserPrefs(UserPrefs userPrefs)
        {
            userPrefsStorage.WriteData(userPrefs);
        }
    }
}
