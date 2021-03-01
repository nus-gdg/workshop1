namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Composite/Sequence", -100)]
    public class Sequence : CompositeNode
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

                if (child.IsStatus(controller, Status.Completed))
                {
                    continue;
                }

                var result = child.Tick(controller);

                if (result != Status.Completed)
                {
                    return result;
                }
            }
            return Status.Completed;
        }
    }
}
