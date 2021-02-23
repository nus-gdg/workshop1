namespace Common.Logic
{
    [CreateNodeMenu("Tasks/Subtree", -80)]
    public class Subtree : TaskNode
    {
        public BehaviourTree tree;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            return tree.root.Evaluate(controller);
        }
    }
}
