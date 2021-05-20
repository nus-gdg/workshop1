using System;
using Project.Controllers;
using UnityEngine;

namespace Project.Views
{
    [Serializable]
    public class Ui : MonoBehaviour, IUi
    {
        [SerializeField]
        private ILogic logic;

        public void Init(ILogic logic)
        {
            this.logic = logic;
        }
    }
}
