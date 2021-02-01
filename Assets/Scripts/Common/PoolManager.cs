using System;
using Entity;
using UnityEngine;

namespace Common
{
    /**
     * Contains a collection of pools that are used in the game.
     * 
     * Example #1: Creating a bullet
     * if (this.Bullets.GetItem(out Bullet bullet))
     * {
     *     bullet.Initialize(param1, param2, ...);
     * }
     */
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private BulletPool bullets;
        public BulletPool Bullets => bullets;

        private void Awake()
        {
            // Place bullet instances as children of the following transform
            bullets.Initialize(transform);
        }
    }
    
    [Serializable] public class BulletPool : Pool<Bullet> { }
}
