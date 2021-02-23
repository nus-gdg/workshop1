namespace Common.Logic
{
    [CreateNodeMenu("Tasks/Subtree", -80)]
    public class Subtree : TaskNode
    {
        public BehaviorTree tree;

        public override Status Evaluate(BehaviorTreeController controller)
        {
            return tree.root.Evaluate(controller);
        }
    }
}
