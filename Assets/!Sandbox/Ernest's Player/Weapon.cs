using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    
    public Plant plant;

    public float bulletForce = 20f;

    public float shotInterval = 1;
    public bool canShoot = true;
    
    public Vector2 target;
    public Vector2 lookDir;


    public Camera cam;

    



    // Update is called once per frame
    void Update()
    {

        lookDir = target - (Vector2)firePoint.position;

    }

    void FixedUpdate()
    {
        


    }

    public void Attack ()
    {
        if (canShoot)
        {
            
            GameObject bullet = Instantiate(plant.bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(lookDir * bulletForce, ForceMode2D.Impulse);
            canShoot = false;
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        if (!canShoot) yield return null;
        yield return new WaitForSeconds(shotInterval);
        canShoot = true;
    }

}
