using UnityEngine;

namespace Project.Views.World.Entities
{
    public class PlayerCursorUi : MonoBehaviour
    {
        // Properties

        [SerializeField]
        private WorldView view;

        public Vector2 Position
        {
            get => transform.position;
            private set => transform.position = value;
        }

        public void Init()
        {
            // Init player cursor in world
        }

        private void Update()
        {
            Position = view.GetWorldMousePosition();
        }
    }
}
