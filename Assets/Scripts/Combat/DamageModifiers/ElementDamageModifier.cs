using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    // optimizable with a new modifier that contains a list of these modifiers and does a lookup for it
    [CreateAssetMenu(fileName = "ElementDamageModifier", menuName = "ScriptableObjects/Combat/DamageModifier/ElementDamageModifier")]
    public class ElementDamageModifier : DamageModifier
    {
        public Element Element;
        public Function Function;

        public override void Evaluate(ref DamageParams damageParams)
        {
            if (Element == damageParams.Element)
            {
                damageParams.DamageAmount = Mathf.RoundToInt(Function.Evaluate(damageParams.DamageAmount));
            }
        }
    }
}
