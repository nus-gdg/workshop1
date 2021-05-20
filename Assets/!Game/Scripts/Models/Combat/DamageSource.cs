using System.Collections.Generic;
using UnityEngine;

namespace Project.Models.Combat
{

    [System.Serializable]
    public struct DamageSource
    {
        public int DamageAmount;
        public Element Element;
        public List<DamageModifier> Modifiers;

        [HideInInspector] // runtime value
        public GameObject SourceEntity;
    }

}
