using System;

namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Decorator/Inverter", -100)]
    public class Inverter : DecoratorNode
    {
        public override Status Evaluate(BehaviourTreeController controller)
        {
            var result = child.Tick(controller);

            switch (result)
            {
                case Status.Completed:
                    return Status.Failed;
    
                case Status.Running:
                    return Status.Running;

                case Status.Failed:
                    return Status.Completed;

                default:
                    throw new InvalidOperationException("Inverter received invalid child status");
            }
        }
    }
}
