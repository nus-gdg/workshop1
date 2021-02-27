using System;
using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Is/float", -100)]
    public class IsFloat : IsNode<float>
    {
        public enum CompareMethod
        {
            Equal, LessThan, GreaterThan,
        }

        [NodeEnum]
        public CompareMethod method;

        public override Status EvaluateCondition(BehaviourTreeController controller)
        {
            switch (method)
            {
                case CompareMethod.Equal:
                    return IsEqual(controller);
                case CompareMethod.LessThan:
                    return IsLessThan(controller);
                case CompareMethod.GreaterThan:
                    return IsGreaterThan(controller);
                default:
                    throw new InvalidOperationException("Unknown integer comparison");
            }
        }

        public Status IsEqual(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(key, out float internalValue))
            {
                return Status.Failed;
            }
            if (Mathf.Approximately(internalValue, value))
            {
                return Status.Completed;
            }
            else
            {
                return Status.Failed;
            }
        }
        
        public Status IsLessThan(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(key, out float internalValue))
            {
                return Status.Failed;
            }
            if (internalValue < value)
            {
                return Status.Completed;
            }
            else
            {
                return Status.Failed;
            }
        }

        public Status IsGreaterThan(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(key, out float internalValue))
            {
                return Status.Failed;
            }
            if (internalValue > value)
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
