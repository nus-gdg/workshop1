using Common;
using UnityEngine;

namespace Entity
{
    public class Player : MonoBehaviour
    {
        // Dependencies
        private InputManager.PlayerActions _controls;
        
        // Components
        [SerializeField]
        private new Rigidbody2D rigidbody;
        
        // Movement variables
        public float speed;
        private Vector2 _direction;
        
        private void Awake()
        {
            _controls = Game.Instance.Input.Player;
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
            var moveInput = _controls.Move.ReadValue<Vector2>();
            _direction = new Vector2(Mathf.RoundToInt(moveInput.x), Mathf.RoundToInt(moveInput.y));
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime * _direction);
        }
    }
}
