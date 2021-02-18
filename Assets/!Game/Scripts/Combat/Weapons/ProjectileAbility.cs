using Combat.Attacks;
using UnityEngine;

namespace Combat.Weapons
{
    [CreateAssetMenu(fileName = "Projectile Ability", menuName = "Weapon/Abilities/Projectile", order = 0)]
    public class ProjectileAbility : Ability
    {
        public float damage = 10f;
        public float force = 20f;

        public override void Execute(Weapon weapon)
        {
            Bullet bullet = Instantiate(bulletPrefab, weapon.firePoint.position, weapon.firePoint.rotation);
            bullet.Rigidbody.AddForce(bullet.transform.up * force, ForceMode2D.Impulse);
            
            weapon.Cooldown(cooldown);
        }
    }
}
