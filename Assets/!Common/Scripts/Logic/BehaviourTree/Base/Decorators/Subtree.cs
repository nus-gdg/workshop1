namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Decorator/Subtree", -100)]
    public class Subtree : ConditionNode
    {
        public BehaviourTree tree;

        public override Status EvaluateCondition(BehaviourTreeController controller)
        {
            return tree.root.Tick(controller);
        }
    }
}
