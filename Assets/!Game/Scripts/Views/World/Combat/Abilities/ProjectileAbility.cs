using UnityEngine;

namespace Project.Views.Combat
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Weapon/Projectile/Shoot", order = 0)]
    public class ProjectileAbility : Ability
    {
        public BulletUi bulletPrefab;
        public int damage = 10;
        public float force = 20f;

        public override void Execute(WeaponUi weapon)
        {
            BulletUi bullet = Instantiate(bulletPrefab, weapon.FirePoint.position, weapon.FirePoint.rotation);
            bullet.Shoot(damage, force);
            weapon.Cooldown(cooldown);
        }
    }
}
