using Project.Models;
using UnityEngine;

namespace Project.Data
{
    public class UserPrefsStorage : MonoBehaviour
    {
        public UserPrefsStorage(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; set; }

        public UserPrefs ReadData()
        {
            return new UserPrefs();
        }

        public void WriteData(UserPrefs data)
        {
            // Write to json file
        }
    }
}
