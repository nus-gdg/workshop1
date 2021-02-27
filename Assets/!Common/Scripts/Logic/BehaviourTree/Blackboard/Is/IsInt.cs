using System;

namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Is/int", -100)]
    public class IsInt : IsNode<int>
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
            if (controller.CompareValue(key, value))
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
            if (!controller.TryGetValue(key, out int internalValue))
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
            if (!controller.TryGetValue(key, out int internalValue))
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
