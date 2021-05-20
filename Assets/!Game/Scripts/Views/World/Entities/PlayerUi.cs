using Project.Views.Common;
using UnityEngine;

namespace Project.Views.World.Entities
{
    public class PlayerUi : MonoBehaviour
    {
        [SerializeField]
        private WorldView view;

        [Header("Visuals")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [Header("Movement")]
        [SerializeField]
        private new Rigidbody2D rigidbody;

        [SerializeField]
        private int maxSpeed;

        [Header("HUD")]
        [SerializeField]
        private InteractableUi interactable;

        [Header("Debug")]
        [SerializeField]
        private bool debug;

        private int _speed = 0;
        private Vector2 _direction = Vector2.down;

        public int MaxSpeed
        {
            get => maxSpeed;
            set => maxSpeed = value;
        }

        public int Speed
        {
            get => _speed;
            set => _speed = (value <= maxSpeed) ? value : maxSpeed;
        }

        public Vector2 Direction
        {
            get => _direction;
            set => _direction = value;
        }

        public Vector2 Position
        {
            get => rigidbody.position;
            set => rigidbody.position = value;
        }

        public void Init()
        {
        }

        private void Update()
        {
            int directionX = 0;
            int directionY = 0;

            if (Input.GetKey(KeyCode.D))
            {
                directionX += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                directionX -= 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                directionY += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                directionY -= 1;
            }

            if (directionX == 0 && directionY == 0)
            {
                Speed = 0;
            }
            else
            {
                Speed = MaxSpeed;
                Direction = new Vector2(directionX, directionY);
            }

            if (Input.GetKeyDown(KeyCode.Space) && interactable != null)
            {
                interactable.Interact();
            }
        }

        private void FixedUpdate()
        {
            // if (view.IsLoadingScene())
            // {
            //     return;
            // }
            rigidbody.MovePosition(Position + Speed * Time.deltaTime * Direction);
            var other = Physics2D.OverlapPoint(Position + Direction.normalized);
            if (other == null)
            {
                interactable = null;
            }
            else
            {
                interactable = other.GetComponent<InteractableUi>();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!debug)
            {
                return;
            }
            if (interactable == null)
            {
                Debug.DrawRay(Position, Direction.normalized, Color.red);
            }
            else
            {
                Debug.DrawRay(Position, Direction.normalized, Color.green);
            }
        }
    }
}
