using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.Weapons
{
    [Serializable]
    public class WeaponEvent : UnityEvent<Weapon> { }

    public class Weapon : MonoBehaviour
    {
        public Transform firePoint;
        public bool canShoot = true;

        public WeaponEvent onAttack;
        public Coroutine cooldown;

        public void Attack()
        {
            if (canShoot)
            {
                onAttack?.Invoke(this);
            }
        }

        public void Aim(Vector2 target)
        {
            Vector2 lookDir = target - (Vector2)transform.position;
            float rotateAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -rotateAngle);
        }

        public void Cooldown(float time)
        {
            if (!canShoot)
            {
                StopCoroutine(cooldown);
            }
            cooldown = StartCoroutine(ShootDelay(time));
        }

        private IEnumerator ShootDelay(float time)
        {
            canShoot = false;
            yield return new WaitForSeconds(time);
            canShoot = true;
        }
    }
}
