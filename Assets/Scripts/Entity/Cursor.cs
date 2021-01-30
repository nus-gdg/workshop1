using Common;
using UnityEngine;

namespace Entity
{
    public class Cursor : MonoBehaviour
    {
        // Dependencies
        private InputManager.PlayerActions _controls;
        private Camera _camera;
        
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
            _camera = Camera.main;
            _transform = transform;
            // Game.Instance.World.Add(this);
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Update()
        {
            var aimInput = _controls.Aim.ReadValue<Vector2>();
            Position = _camera.ScreenToWorldPoint(aimInput);
        }
    }
}
