namespace Common.Logic
{
    public interface ISetNode
    {
        BehaviourTreeNode.Status Evaluate(BehaviourTreeController controller);
    }

    public abstract class SetNode<T> : TaskNode, ISetNode
    {
        public BlackboardKey key;
        public T value;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            controller[key] = value;
            return Status.Completed;
        }
    }
}
