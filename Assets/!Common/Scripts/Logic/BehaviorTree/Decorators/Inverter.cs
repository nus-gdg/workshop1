using System;

namespace Common.Logic
{
    [CreateNodeMenu("Decorator/Inverter", -90)]
    public class Inverter : DecoratorNode
    {
        public override Status Evaluate(BehaviorTreeController controller)
        {
            var result = child.Evaluate(controller);
            controller.RegisterNodeStatus(child, result);

            switch (result)
            {
                case Status.Completed:
                    return Status.Failed;
    
                case Status.Running:
                    return Status.Running;

                case Status.Failed:
                    return Status.Completed;

                default:
                    throw new InvalidOperationException("Current node received unknown status");
            }
        }
    }
}
