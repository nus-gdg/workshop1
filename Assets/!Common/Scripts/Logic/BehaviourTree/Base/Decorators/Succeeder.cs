namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Decorator/Succeeder", -100)]
    public class Succeeder : DecoratorNode
    {
        public override Status Evaluate(BehaviourTreeController controller)
        {
            var result = child.Tick(controller);
            if (result == Status.Running)
            {
                return Status.Running;
            }
            return Status.Completed;
        }
    }
}
