using UnityEngine;
using UnityEngine.Assertions;

namespace Combat
{

    [RequireComponent(typeof(Collider2D))]
    public class HitBox : MonoBehaviour
    {
        public DamageSource DamageSource;
        private Collider2D _col;

        private void OnEnable()
        {
            _col.enabled = true;
        }
        private void OnDisable()
        {
            _col.enabled = false;
        }

        void Awake()
        {
            _col = GetComponent<Collider2D>();
            Assert.IsTrue(_col.isTrigger, "HurtBox expects a trigger collider");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            HurtBox hurtBox = other.GetComponent<HurtBox>();
            if (hurtBox != null)
            {
                DamageSource damageSource = DamageSource;
                damageSource.SourceEntity = gameObject;
                hurtBox.ApplyDamage(damageSource);
            }
        }
    }

}
