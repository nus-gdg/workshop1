using System.Collections;
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
                bullet.Rigidbody.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
            }
        }

        IEnumerator ShootDelay()
        {
            canShoot = false;
            yield return new WaitForSeconds(shotInterval);
            canShoot = true;
        }
    }
}
