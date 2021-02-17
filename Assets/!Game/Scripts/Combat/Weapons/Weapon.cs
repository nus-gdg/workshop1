﻿using System.Collections;
using Combat.Attacks;
using UnityEngine;

namespace Combat.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public Transform firePoint;
        public Bullet bulletPrefab;

        public float bulletForce = 20f;

        public float shotInterval = 1;
        public bool canShoot = true;

        public void Attack ()
        {
            if (canShoot)
            {
                StartCoroutine(ShootDelay());

                Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.Rigidbody.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
            }
        }

        public void Aim(Vector2 target)
        {
            Vector2 lookDir = target - (Vector2)transform.position;
            float rotateAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -rotateAngle);
        }

        IEnumerator ShootDelay()
        {
            canShoot = false;
            yield return new WaitForSeconds(shotInterval);
            canShoot = true;
        }
    }
}