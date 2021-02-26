using UnityEngine;

namespace Model
{
    public abstract class Entity : MonoBehaviour
    {
        // --- Components ---
        [SerializeField]
        private new Rigidbody2D rigidbody;
        public Rigidbody2D Rigidbody => rigidbody;

        [SerializeField]
        private new Collider2D collider;
        public Collider2D Collider => collider;

        [SerializeField]
        private float speed;
        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        [SerializeField]
        private Vector2 direction;
        public Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }
    }
}
