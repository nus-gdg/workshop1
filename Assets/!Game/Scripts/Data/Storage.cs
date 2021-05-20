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
    }
}
