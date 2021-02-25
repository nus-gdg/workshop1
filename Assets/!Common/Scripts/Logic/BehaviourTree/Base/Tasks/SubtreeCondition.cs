namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Tasks/Subtree/Condition", -100)]
    public class SubtreeCondition : ConditionNode
    {
        public BehaviourTree tree;

        public override Status EvaluateCondition(BehaviourTreeController controller)
        {
            return tree.root.Evaluate(controller);
        }
    }
}
