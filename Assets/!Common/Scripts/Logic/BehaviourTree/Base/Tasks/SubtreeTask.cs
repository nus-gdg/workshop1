namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Tasks/Subtree/Task", -100)]
    public class SubtreeTask : TaskNode
    {
        public BehaviourTree tree;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            return tree.root.Evaluate(controller);
        }
    }
}
