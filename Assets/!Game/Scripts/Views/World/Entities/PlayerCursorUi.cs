using UnityEngine;

namespace Project.Views.World.Entities
{
    public class PlayerCursorUi : MonoBehaviour
    {
        // Properties

        private WorldView _view;

        public Vector2 Position
        {
            get => transform.position;
            private set => transform.position = value;
        }

        public void Init(WorldView view)
        {
            _view = view;
            // Init player cursor in world
        }

        private void Update()
        {
            Position = _view.GetWorldMousePosition();
        }
    }
}
