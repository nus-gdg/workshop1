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
            _transform = transform;

            Game.Instance.World.Cursor = this;
        }

        private void Update()
        {
            var aimInput = _controls.Aim.ReadValue<Vector2>();
            Position = Game.Instance.World.Camera.AttachedCamera.ScreenToWorldPoint(aimInput);
        }
    }
}
