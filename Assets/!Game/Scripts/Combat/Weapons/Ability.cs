using Combat.Attacks;
using UnityEngine;

namespace Combat.Weapons
{
    public abstract class Ability : ScriptableObject
    {
        public Bullet bulletPrefab;
        public float cooldown;

        public abstract void Execute(Weapon weapon);
    }
}
