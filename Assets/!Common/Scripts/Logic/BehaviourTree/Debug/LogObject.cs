using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Debug/Log/Object", -100)]
    public class LogObject : TaskNode
    {
        public Object value;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            Debug.Log(value);
            return Status.Completed;
        }
    }
}
