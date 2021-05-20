using Project.Models.Combat;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Views.Combat
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(DamageReceiverUi))]
    public class HurtBoxUi : MonoBehaviour
    {
        private DamageReceiverUi damageReceiver;

        void Awake()
        {
            damageReceiver = GetComponent<DamageReceiverUi>();
            Collider2D col = GetComponent<Collider2D>();
            Assert.IsTrue(col.isTrigger, "HurtBox expects a trigger collider");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            HitBoxUi hitbox = other.GetComponent<HitBoxUi>();
            if (hitbox != null)
            {
                DamageSource damageSource = hitbox.DamageSource;
                if (damageSource.SourceEntity == null)
                {
                    damageSource.SourceEntity = other.gameObject;
                }
                damageReceiver.ApplyDamage(damageSource);
            }
        }
    }
}
