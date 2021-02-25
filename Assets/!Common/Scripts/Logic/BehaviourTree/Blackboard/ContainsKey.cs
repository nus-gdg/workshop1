namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Contains", -100)]
    public class ContainsKey : TaskNode
    {
        public BlackboardKey key;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (controller.ContainsKey(key))
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
