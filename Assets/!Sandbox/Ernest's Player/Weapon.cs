using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    
    public Plant plant;

    public float bulletForce = 20f;

    public float shotInterval = 1;
    public bool canShoot = true;

    public void Attack ()
    {
        if (canShoot)
        {
            StartCoroutine(ShootDelay());

            GameObject bullet = Instantiate(plant.bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
        }
    }

    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotInterval);
        canShoot = true;
    }
}
