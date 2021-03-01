namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Composite/Active Selector", -100)]
    public class ActiveSelector : CompositeNode
    {
        public override Status Evaluate(BehaviourTreeController controller)
        {
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                if (child == null)
                {
                    continue;
                }

                var result = child.Tick(controller);

                if (result != Status.Failed)
                {
                    return result;
                }
            }
            return Status.Failed;
        }
    }
}
