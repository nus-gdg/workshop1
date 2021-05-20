using System;
using System.IO;
using UnityEngine;

namespace Project.Common
{
    [Serializable]
    public class Config : MonoBehaviour
    {
        [field: SerializeField] public string SaveDataFilePath { get; private set; } = "save.json";
        [field: SerializeField] public string UserPrefsFilePath { get; private set; } = "preferences.json";

        public string FilePath { get; private set; }

        public void Init(string configFilePath)
        {
            FilePath = configFilePath;

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, JsonUtility.ToJson(this, prettyPrint:true));
                return;
            }

            string json = File.ReadAllText(FilePath);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}
