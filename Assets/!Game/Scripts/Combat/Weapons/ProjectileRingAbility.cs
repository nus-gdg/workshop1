using Combat.Attacks;
using UnityEngine;

namespace Combat.Weapons
{
    [CreateAssetMenu(fileName = "Projectile Ring Ability", menuName = "Weapon/Abilities/Projectile Ring", order = 0)]
    public class ProjectileRingAbility : Ability
    {
        public float damage = 10f;
        public float force = 20f ;

        public int size = 8 ;

        public override void Execute(Weapon weapon)
        {
            var origin = weapon.firePoint.position;
            var angleInterval = 360f / size;

            for (float angle = 0; angle < 360; angle += angleInterval)
            {
                Shoot(origin, Quaternion.Euler(0, 0, angle));
            }
        }

        private void Shoot(Vector3 origin, Quaternion rotation)
        {
            Bullet bullet = Instantiate(bulletPrefab, origin, rotation);
            bullet.Rigidbody.AddForce(bullet.transform.up * force, ForceMode2D.Impulse);
        }
    }
}
