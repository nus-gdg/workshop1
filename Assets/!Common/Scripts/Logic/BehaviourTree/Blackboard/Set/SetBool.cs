using System;

namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Set/bool", -100)]
    public class SetBool : TaskNode, ISetNode
    {
        public enum BoolOperation
        {
            True, False, Toggle,
        }

        public BlackboardKey key;

        [NodeEnum]
        public BoolOperation value;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            switch (value)
            {
                case BoolOperation.True:
                    controller[key] = true;
                    break;
                case BoolOperation.False:
                    controller[key] = false;
                    break;
                case BoolOperation.Toggle:
                    if (controller.TryGetValue(key, out bool internalValue))
                    {
                        controller[key] = !internalValue;
                    }
                    else
                    {
                        controller[key] = true;
                    }
                    break;
                default:
                    throw new InvalidOperationException("Unknown boolean operation");
            }
            return Status.Completed;
        }
    }
}
