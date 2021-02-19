using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public Transform Transform { get; private set; }

        [SerializeField]
        private Transform firePoint;
        public Transform FirePoint
        {
            get => firePoint;
            set => firePoint = value;
        }

        public float shotInterval = 1;

        public bool canShoot = true;
        
        [SerializeField]
        private WeaponEvent onAttack;

        public void Awake()
        {
            Transform = transform;
        }

        public void Attack()
        {
            if (canShoot)
            {
                onAttack?.Invoke(this);
            }
        }

        public void Aim(Vector2 target)
        {
            Vector2 lookDir = target - (Vector2)Transform.position;
            float rotateAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            Transform.rotation = Quaternion.Euler(0, 0, -rotateAngle);
        }

        IEnumerator ShootDelay()
        {
            canShoot = false;
            yield return new WaitForSeconds(shotInterval);
            canShoot = true;
        }

        [Serializable]
        protected class WeaponEvent : UnityEvent<Weapon> { }
    }
}
