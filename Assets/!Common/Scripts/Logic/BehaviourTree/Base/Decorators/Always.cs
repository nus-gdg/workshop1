using System;

namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Decorator/Always", -100)]
    public class Always : DecoratorNode
    {
        public enum AlwaysStatus
        {
            Completed, Running, Failed,
        }

        [NodeEnum]
        public AlwaysStatus status;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (child != null)
            {
                child.Tick(controller);
            }

            switch (status)
            {
                case AlwaysStatus.Completed:
                    return Status.Completed;
                case AlwaysStatus.Running:
                    return Status.Running;
                case AlwaysStatus.Failed:
                    return Status.Failed;
                default:
                    throw new InvalidOperationException("Always node has an invalid status");
            }
        }
    }
}
