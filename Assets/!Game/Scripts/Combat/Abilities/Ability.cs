using Combat.Weapons;
using UnityEngine;

namespace Combat.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        public float cooldown;

        public abstract void Execute(Weapon weapon);
    }
}
