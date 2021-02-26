namespace Common.Logic
{
    public abstract class ConditionNode : DecoratorNode
    {
        public override Status Evaluate(BehaviourTreeController controller)
        {
            var condition = EvaluateCondition(controller);
            if (condition != Status.Completed)
            {
                return condition;
            }
            if (child == null)
            {
                return Status.Failed;
            }

            var result = child.Evaluate(controller);
            controller.RegisterNodeStatus(child, result);
            return result;
        }

        public abstract Status EvaluateCondition(BehaviourTreeController controller);
    }
}
