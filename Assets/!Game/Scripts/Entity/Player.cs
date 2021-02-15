using Combat.Weapons;
using Core;
using Core.Managers;
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
        [SerializeField]
        private Weapon weapon;

        // Movement variables
        public float speed;
        private Vector2 _direction;

        private void Awake()
        {
            _controls = Game.Instance.Input.Player;
            Game.Instance.World.Player = this;
        }

        private void Update()
        {
            var moveInput = _controls.Move.ReadValue<Vector2>();
            _direction = new Vector2(Mathf.RoundToInt(moveInput.x), Mathf.RoundToInt(moveInput.y));

            weapon.Aim(Game.Instance.World.Cursor.Position);

            if (_controls.Action1.ReadValue<float>() > 0f)
            {
                weapon.Attack();
            }
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime * _direction);
        }
    }
}
