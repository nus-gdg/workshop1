using Project.Models;
using UnityEngine;

namespace Project.Data
{
    public class SaveDataStorage : MonoBehaviour
    {
        public SaveDataStorage(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; set; }

        public SaveData ReadData()
        {
            return new SaveData();
        }

        public void WriteData(SaveData data)
        {
            // Write to json file
        }
    }
}
