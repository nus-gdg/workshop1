namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Copy", -100)]
    public class CopyKey : TaskNode
    {
        public BlackboardKey from;
        public BlackboardKey to;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            controller[to] = controller[from];
            return Status.Completed;
        }
    }
}
