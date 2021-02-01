using UnityEngine;
using Common;

namespace Entity
{
    public class Bullet : Poolable
    {
        [SerializeField]
        private new Rigidbody2D rigidbody;

        public Vector2 velocity;

        protected override void OnAwake()
        {
        }

        protected override void OnRecycle()
        {
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            rigidbody.position += velocity * Time.fixedDeltaTime;
        }

        private void OnTriggerExit(Collider other)
        {
            Recycle();
        }
    }
}
