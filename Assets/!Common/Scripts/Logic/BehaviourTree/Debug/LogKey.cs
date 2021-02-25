using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Debug/Log/Key", -100)]
    public class LogKey : TaskNode
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
