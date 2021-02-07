using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Combat
{
    public abstract class DamageModifier : ScriptableObject
    {
        public DamageStep DamageStep;
        public abstract void Evaluate(ref DamageParams damageParams);
    }
}
