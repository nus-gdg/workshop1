using UnityEngine;

namespace Entity.Events
{
    [CreateAssetMenu(fileName = "DebugLog", menuName = "Events/Debug/Log")]
    public class DebugLogEvent : ScriptableObject
    {
        public void Execute(string message)
        {
            Debug.Log(message);
        }
    }
}
