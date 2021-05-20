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
    }
}
