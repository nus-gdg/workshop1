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
    }
}
