using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public class EntityStats : MonoBehaviour
    {
        public List<DamageModifier> OffensiveDamageModifiers = new List<DamageModifier>();
        public List<DamageModifier> DefensiveDamageModifiers = new List<DamageModifier>();
    }

}
