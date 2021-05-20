using UnityEngine;

namespace Project.Views.Combat
{
    [CreateAssetMenu(fileName = "ProjectileRing", menuName = "Weapon/Projectile/Ring", order = 0)]
    public class ProjectileRingAbility : Ability
    {
        public BulletUi bulletPrefab;
        public int damage = 10;
        public float force = 20f ;

        public int size = 8 ;

        public override void Execute(WeaponUi weapon)
        {
            var origin = weapon.FirePoint.position;
            var angleInterval = 360f / size;

            for (float angle = 0; angle < 360; angle += angleInterval)
            {
                BulletUi bullet = Instantiate(bulletPrefab, origin, Quaternion.Euler(0, 0, angle));
                bullet.Shoot(damage, force);
            }
            
            weapon.Cooldown(cooldown);
        }
    }
}
