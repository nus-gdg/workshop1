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
        }

        public void ApplyDamage(DamageSource damageSource)
        {
            damageReceiver.ApplyDamage(damageSource);
        }
    }

}
