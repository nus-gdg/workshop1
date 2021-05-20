namespace Project.Models
{
    public interface IModel
    {
        void Init(SaveData saveData, UserPrefs userPrefs);

        SaveData GetSaveData();
        UserPrefs GetUserPrefs();
    }
}
