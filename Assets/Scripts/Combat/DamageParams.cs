using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public struct DamageParams
    {
        public int DamageAmount;
        public Element Element;
        public CombatStats SourceStats;
        public CombatStats TargetStats;
        public GameObject SourceEntity;
        public GameObject TargetEntity;
    }
}

