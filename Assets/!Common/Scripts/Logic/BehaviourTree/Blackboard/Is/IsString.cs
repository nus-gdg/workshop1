using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Is/string", -100)]
    public class IsString : ConditionNode, IIsNode
    {
        public BlackboardKey key;
        
        [TextArea]
        public string value;

        public override Status EvaluateCondition(BehaviourTreeController controller)
        {
            if (controller.CompareValue(key, value))
            {
                return Status.Completed;
            }
            else
            {
                return Status.Failed;
            }
        }
    }
}
