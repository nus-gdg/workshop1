namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Composite/Parallel", -100)]
    public class Parallel : CompositeNode
    {
        public enum ParallelPolicy
        {
            RequireOne, RequireAll,
        }

        [NodeEnum]
        public ParallelPolicy complete;

        [NodeEnum]
        public ParallelPolicy fail;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            int numChildren = 0;
            int numCompleted = 0;
            int numFailed = 0;

            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                if (child == null)
                {
                    continue;
                }
                numChildren++;

                var result = child.Tick(controller);
                switch (result)
                {
                    case Status.Completed:
                        if (complete == ParallelPolicy.RequireOne)
                        {
                            return Status.Completed;
                        }
                        numCompleted++;
                        break;
                    case Status.Failed:
                        if (fail == ParallelPolicy.RequireOne)
                        {
                            return Status.Failed;
                        }
                        numFailed++;
                        break;
                    case Status.Running:
                    default:
                        break;
                }
            }

            if (fail == ParallelPolicy.RequireAll && numFailed == numChildren)
            {
                return Status.Failed;
            }

            if (complete == ParallelPolicy.RequireAll && numCompleted == numChildren)
            {
                return Status.Completed;
            }
            return Status.Running;
        }
    }
}
