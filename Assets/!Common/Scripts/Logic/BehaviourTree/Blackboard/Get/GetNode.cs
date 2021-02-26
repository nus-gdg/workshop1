namespace Common.Logic
{
    public abstract class GetNode<T, TVariable> : TaskNode
        where TVariable : Variable<T>
    {
        public BlackboardKey key;
        public TVariable variable;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            controller[key] = variable.Value;
            return Status.Completed;
        }
    }
}
