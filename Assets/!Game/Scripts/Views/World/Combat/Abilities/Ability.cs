using UnityEngine;

namespace Project.Views.Combat
{
    public abstract class Ability : ScriptableObject
    {
        public float cooldown;

        public abstract void Execute(WeaponUi weapon);
    }
}
