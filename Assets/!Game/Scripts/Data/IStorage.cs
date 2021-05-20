namespace Project.Data
{
    public interface IStorage
    {
        void Init(string saveDataFilePath, string userPrefsFilePath);
    }
}
