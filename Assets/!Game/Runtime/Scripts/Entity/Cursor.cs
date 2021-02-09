using Core;
using Core.Managers;
using UnityEngine;
using UnityEngine.Assertions;

namespace World.Ui
{
    public class Cursor : MonoBehaviour
    {
        // Dependencies
        private InputManager.PlayerActions _controls;
        private UnityEngine.Camera _camera;

        // Components
        // [SerializeField]
        // private new Rigidbody2D rigidbody;

        // Properties
        private Transform _transform;
        public Vector2 Position
        {
            get => _transform.position;
            private set => _transform.position = value;
        }

        private void Awake()
        {
            _controls = Game.Instance.Input.Player;
            _camera = UnityEngine.Camera.main;
            _transform = transform;

            // Game.Instance.World.Add(this);
            Assert.IsNull(Game.Instance.World.Cursor, "Cursor.Awake Cursor registed in world is not null");
            Game.Instance.World.Cursor = this;
        }

        private void Update()
        {
            var aimInput = _controls.Aim.ReadValue<Vector2>();
            Position = _camera.ScreenToWorldPoint(aimInput);
            Position = Game.Instance.World.Camera.ClampWorldPositionInsideCamera2D(Position);
        }
    }
}