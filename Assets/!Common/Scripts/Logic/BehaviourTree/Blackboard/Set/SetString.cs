using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Set/string", -100)]
    public class SetString : TaskNode, ISetNode
    {
        public BlackboardKey key;
        
        [TextArea]
        public string value;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            controller[key] = value;
            return Status.Completed;
        }
    }
}
