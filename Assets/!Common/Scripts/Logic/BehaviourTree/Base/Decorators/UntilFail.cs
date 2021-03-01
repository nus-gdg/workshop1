namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Decorator/Until Fail", -100)]
    public class UntilFail : DecoratorNode
    {
        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (child == null)
            {
                return Status.Completed;
            }
            
            var result = child.Tick(controller);
            if (result == Status.Failed)
            {
                return Status.Completed;
            }

            return Status.Running;
        }
    }
}
