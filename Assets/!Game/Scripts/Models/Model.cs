using System;
using UnityEngine;

namespace Project.Models
{
    [Serializable]
    public class Model : MonoBehaviour, IModel
    {
        [SerializeField]
        private SaveData saveData;

        [SerializeField]
        private UserPrefs userPrefs;

        public void Init(SaveData saveData, UserPrefs userPrefs)
        {
            // Initialise model from files
            this.saveData = saveData;
            this.userPrefs = userPrefs;
        }

        public SaveData GetSaveData()
        {
            return saveData;
        }

        public UserPrefs GetUserPrefs()
        {
            return userPrefs;
        }
    }
}
