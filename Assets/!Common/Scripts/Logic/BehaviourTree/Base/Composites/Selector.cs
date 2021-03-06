namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Composite/Selector", -100)]
    public class Selector : CompositeNode
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

                if (child.IsStatus(controller, Status.Failed))
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
