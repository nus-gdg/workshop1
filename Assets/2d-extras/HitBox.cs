using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Combat
{

    [RequireComponent(typeof(Collider2D))]
    public class HitBox : MonoBehaviour
    {
        public DamageSource DamageSource;

        void Awake()
        {
            Collider2D col = GetComponent<Collider2D>();
            Assert.IsTrue(col.isTrigger, "HurtBox expects a trigger collider");
        }
    }

}
