using UnityEngine;

namespace Combat.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private new Collider2D collider;
        public Collider2D Collider => collider;

        [SerializeField]
        private new Rigidbody2D rigidbody;
        public Rigidbody2D Rigidbody => rigidbody;

        [SerializeField]
        private HitBox hitBox;
        public HitBox HitBox => hitBox;
    
        private void Start()
        {
            if (collider == null)
            {
                collider = GetComponent<Collider2D>();
                if (collider == null)
                {
                    throw new MissingComponentException("Missing component: Collider2D");
                }
            }
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
                if (rigidbody == null)
                {
                    throw new MissingComponentException("Missing component: Rigidbody2D");
                }
            }
            if (hitBox == null)
            {
                hitBox = GetComponent<HitBox>();
                if (hitBox == null)
                {
                    throw new MissingComponentException("Missing component: HitBox");
                }
            }
        }
        
        public void Shoot(int damage, float force)
        {
            HitBox.DamageSource.DamageAmount = damage;
            Rigidbody.AddForce(transform.up * force, ForceMode2D.Impulse);
        }
    
        void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
        }
    }
}
