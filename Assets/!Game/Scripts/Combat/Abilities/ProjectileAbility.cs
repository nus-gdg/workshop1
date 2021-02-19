using Combat.Weapons;
using UnityEngine;

namespace Combat.Abilities
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Weapon/Projectile/Shoot", order = 0)]
    public class ProjectileAbility : Ability
    {
        public Bullet bulletPrefab;
        public int damage = 10;
        public float force = 20f;

        public override void Execute(Weapon weapon)
        {
            Bullet bullet = Instantiate(bulletPrefab, weapon.FirePoint.position, weapon.FirePoint.rotation);
            bullet.Shoot(damage, force);
            weapon.Cooldown(cooldown);
        }
    }
}
