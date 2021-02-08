using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Combat
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(DamageReceiver))]
    public class HurtBox : MonoBehaviour
    {
        private DamageReceiver damageReceiver;
        void Awake()
        {
            damageReceiver = GetComponent<DamageReceiver>();
            Collider2D col = GetComponent<Collider2D>();
            Assert.IsTrue(col.isTrigger, "HurtBox expects a trigger collider");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            HitBox hitbox = other.GetComponent<HitBox>();
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
