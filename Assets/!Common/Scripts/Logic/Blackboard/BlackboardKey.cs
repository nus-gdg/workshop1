using UnityEngine;

namespace Common.Logic
{
    [CreateAssetMenu(fileName = "Blackboard Key", menuName = "Blackboard Key")]
    public class BlackboardKey : ScriptableObject
    {
        public int Id => GetInstanceID();
    }
}
