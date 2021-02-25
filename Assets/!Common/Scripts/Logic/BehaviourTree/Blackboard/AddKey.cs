namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Add", -100)]
    public class AddKey : TaskNode
    {
        public BlackboardKey key;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            controller[key] = null;
            return Status.Completed;
        }
    }
}
