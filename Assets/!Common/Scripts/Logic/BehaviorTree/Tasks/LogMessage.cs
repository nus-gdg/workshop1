using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Tasks/Log Message", -80)]
    public class LogMessage : TaskNode
    {
        public string message;

        public override Status Evaluate(BehaviorTreeController controller)
        {
            Debug.Log(message);
            return Status.Completed;
        }
    }
}
