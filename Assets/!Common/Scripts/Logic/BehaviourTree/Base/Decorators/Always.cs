namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Decorator/Always", -100)]
    public class Always : DecoratorNode
    {
        [NodeEnum]
        public Status status;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (child != null)
            {
                var result = child.Evaluate(controller);
                controller.RegisterNodeStatus(child, result);
            }
            return status;
        }
    }
}
