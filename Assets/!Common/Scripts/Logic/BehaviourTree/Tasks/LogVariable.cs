using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Tasks/Log Variable", -80)]
    public class LogVariable : TaskNode
    {
        public BlackboardKey key;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(key, out object value))
            {
                return Status.Failed;
            }
            Debug.Log(value);
            return Status.Completed;
        }
    }
}
