namespace Common.Logic
{
    [CreateNodeMenu("Decorator/Succeeder", -90)]
    public class Succeeder : DecoratorNode
    {
        public override Status Evaluate(BehaviorTreeController controller)
        {
            var result = child.Evaluate(controller);
            controller.RegisterNodeStatus(child, result);

            if (result == Status.Running)
            {
                return Status.Running;
            }
            return Status.Completed;
        }
    }
}
