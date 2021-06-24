using System;
using System.Collections.Generic;
using Project.Views.Combat;
using UnityEngine;

namespace Project.Views.World.Entities
{
    public class PlayerUi : MonoBehaviour
    {
        [SerializeField]
        private WorldView view;

        [SerializeField]
        private new Rigidbody2D rigidbody;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private WeaponUi weapon;

        [SerializeField]
        public PlayerInput input;

        [SerializeField]
        private bool debug;

        private PlayerState _state;

        private Vector3 _direction;
        private float _speed;

        public bool IsMoving { get; private set; } = false;

        public Vector3 Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                animator.SetFloat("inputX", _direction.x);
                animator.SetFloat("inputY", _direction.y);
            }
        }

        public Vector3 Position
        {
            get => rigidbody.position;
            set => rigidbody.position = value;
        }

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
            }
        }

        public void Init(WorldView view)
        {
            this.view = view;

            Direction = Vector3.down;

            _state = new PlayerIdleState(false);
            _state.Enter(this);
        }

        public void ChangeState(PlayerState state)
        {
            _state.Exit(this);
            _state = state;
            _state.Enter(this);
        }

        public void PlayAnimation(string animation)
        {
            animator.Play(animation);
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(Position + Speed * Time.deltaTime * Direction);
        }

        private void Update()
        {
            _state.Update(this);

            if (debug)
            {
                Debug.Log(_state.name);
            }
        }

        public void UpdateMovement()
        {
            int directionX = 0;
            int directionY = 0;

            if (Input.GetKey(input.left.keyCode))
            {
                directionX -= 1;
            }
            if (Input.GetKey(input.right.keyCode))
            {
                directionX += 1;
            }
            if (Input.GetKey(input.down.keyCode))
            {
                directionY -= 1;
            }
            if (Input.GetKey(input.up.keyCode))
            {
                directionY += 1;
            }

            IsMoving = directionX != 0 || directionY != 0;
            if (IsMoving)
            {
                Direction = new Vector3(directionX, directionY);
            }
        }

        public void UpdateWeapon()
        {
            weapon.Aim(view.GetWorldMousePosition());

            if (Input.GetMouseButtonDown(0))
            {
                weapon.Charge();
                weapon.Attack();
            }

            if (Input.GetMouseButton(0))
            {
                weapon.Autofire();
            }

            if (Input.GetMouseButtonUp(0))
            {
                weapon.Unleash();
            }
        }

        public void UpdateInput()
        {
            var currentTime = Time.time;

            foreach (var binding in input.Bindings)
            {
                if (Input.GetKeyDown(binding.keyCode))
                {
                    binding.timeSinceLastKeyDown = currentTime;
                }
                if (Input.GetKeyUp(binding.keyCode))
                {
                    binding.timeSinceLastKeyUp = currentTime;
                }
            }
        }

        [Serializable]
        public class PlayerInput
        {
            public InputBinding up;
            public InputBinding left;
            public InputBinding down;
            public InputBinding right;

            public List<InputBinding> Bindings => new List<InputBinding>()
            {
                up, left, down, right,
            };
        }

        [Serializable]
        public class InputBinding
        {
            public KeyCode keyCode;

            public float timeSinceLastKeyDown;
            public float timeSinceLastKeyUp;

            public float doubleTapThreshold;

            public bool HasDoubleTap()
            {
                var tapDelay = timeSinceLastKeyDown - timeSinceLastKeyUp;
                return tapDelay <= doubleTapThreshold && tapDelay > 0;
            }
        }
    }
}
