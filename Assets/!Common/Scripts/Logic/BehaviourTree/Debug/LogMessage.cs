using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Debug/Log/Message", -100)]
    public class LogMessage : TaskNode
    {
        [NodeEnum]
        public LogType type = LogType.Log;

        [TextArea(1, int.MaxValue)]
        public string message;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            Debug.LogFormat(type, LogOption.None, null, message);
            return Status.Completed;
        }
    }
}
