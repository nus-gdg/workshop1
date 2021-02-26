namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Remove", -100)]
    public class RemoveKey : TaskNode
    {
        public BlackboardKey key;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            controller.RemoveValue(key);
            return Status.Completed;
        }
    }
}
