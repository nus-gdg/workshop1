using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "CombatStats", menuName = "ScriptableObjects/Combat/CombatStats")]
    public class CombatStats : ScriptableObject
    {
        // we can put other related info here
        public List<DamageModifier> DamageModifiers;
    }

}
