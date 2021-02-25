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

                var result = child.Evaluate(controller);
                controller.RegisterNodeStatus(child, result);

                if (result != Status.Failed)
                {
                    return result;
                }
            }
            return Status.Failed;
        }
    }
}
