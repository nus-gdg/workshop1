namespace Common.Logic
{
    public abstract class ConditionNode : DecoratorNode
    {
        public override Status Evaluate(BehaviourTreeController controller)
        {
            var result = EvaluateCondition(controller);
            if (result != Status.Completed)
            {
                return result;
            }
            if (child == null)
            {
                return result;
            }

            return child.Tick(controller);
        }

        public abstract Status EvaluateCondition(BehaviourTreeController controller);
    }
}
