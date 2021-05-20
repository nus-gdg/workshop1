using Project.Controllers;
using UnityEngine;

namespace Project.Views
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField]
        protected IUi ui;

        [SerializeField]
        protected ILogic logic;

        private void Start()
        {
            ui = Game.Instance.Ui;
            logic = Game.Instance.Logic;

            Init();
        }

        public virtual void Init()
        {

        }
    }
}
