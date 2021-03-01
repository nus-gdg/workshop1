using Combat.Weapons;
using UnityEngine;
using System.Collections;

namespace Combat.Abilities
{
    [CreateAssetMenu(fileName = "SpiralShot", menuName = "Weapon/Projectile/SpiralShot", order = 0)]
    public class SpiralShot : Ability
    {
        public Bullet bulletPrefab;
        public int bullets = 30;
        public int spirals = 3;
        public float force = 2f;
        public int damage = 10;
        public float interval = 0.1f;

        public override void Execute(Weapon weapon)
        {
            weapon.StartCoroutine(SpiralShotEnumerator(weapon));
            weapon.Cooldown(cooldown);
        }

        IEnumerator SpiralShotEnumerator(Weapon weapon)
        {
            var totalangle = spirals * 360f;
            var angleInterval = totalangle / bullets;

            for (float angle = 0; angle < totalangle; angle += angleInterval)
            {
                var origin = weapon.FirePoint.position;
                Bullet bullet = Instantiate(bulletPrefab, origin, Quaternion.Euler(0, 0, angle));
                bullet.Shoot(damage, force);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}