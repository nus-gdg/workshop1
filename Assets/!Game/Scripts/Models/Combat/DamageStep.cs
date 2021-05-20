using UnityEngine;

namespace Project.Models.Combat
{

    [CreateAssetMenu(fileName = "DamageStep", menuName = "ScriptableObjects/Combat/DamageStep")]
    public class DamageStep : ScriptableObject
    {
        public int Priority { get; set; }
    }

}
