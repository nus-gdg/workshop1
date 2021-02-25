namespace Common.Logic
{
    public interface IIsNode
    {
        BehaviourTreeNode.Status EvaluateCondition(BehaviourTreeController controller);
    }

    public abstract class IsNode<T> : ConditionNode, IIsNode
    {
        public BlackboardKey key;
        public T value;

        public override Status EvaluateCondition(BehaviourTreeController controller)
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
    }
}
