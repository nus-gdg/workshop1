using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    [CreateAssetMenu(fileName = "DamageStep", menuName = "ScriptableObjects/Combat/DamageStep")]
    public class DamageStep : ScriptableObject
    {
        public int Priority { get; set; }
    }

}
