using Project.Models;

namespace Project.Data
{
    public interface IStorage
    {
        void Init(string saveDataFilePath, string userPrefsFilePath);

        SaveData ReadSaveData();
        UserPrefs ReadUserPrefs();

        void WriteSaveData(SaveData saveData);
        void WriteUserPrefs(UserPrefs userPrefs);
    }
}
