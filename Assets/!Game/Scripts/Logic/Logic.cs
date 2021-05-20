using System;
using Project.Data;
using Project.Models;
using UnityEngine;

namespace Project.Controllers
{
    [Serializable]
    public class Logic : MonoBehaviour, ILogic
    {
        [SerializeField]
        private IModel model;

        [SerializeField]
        private IStorage storage;

        [SerializeField]
        private bool debug;

        public void Init(IModel model, IStorage storage)
        {
            this.model = model;
            this.storage = storage;
        }

        public CommandResult Execute(Command command)
        {
            if (debug)
            {
                Debug.Log($"Executing: [{command.Type}]\n\n{command.Info}");
            }
            CommandResult result = command.Execute(model);
            return result;
        }
    }
}
