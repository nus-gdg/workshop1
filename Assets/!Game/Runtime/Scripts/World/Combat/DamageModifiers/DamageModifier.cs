using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Combat
{
    public struct DamageModifierRuntimeParams
    {
        public int DamageAmount;
        public Element Element;
        public GameObject SourceEntity;
        public GameObject TargetEntity;
    }

    public abstract class DamageModifier : ScriptableObject
    {
        public DamageStep DamageStep;
        public abstract void Evaluate(ref DamageModifierRuntimeParams damageParams);
    }
}
