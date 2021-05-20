using Project.Models.Combat;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Views.Combat
{

    [RequireComponent(typeof(Collider2D))]
    public class HitBoxUi : MonoBehaviour
    {
        public DamageSource DamageSource;

        void Awake()
        {
            Collider2D col = GetComponent<Collider2D>();
            Assert.IsTrue(col.isTrigger, "HurtBox expects a trigger collider");
        }
    }
}
