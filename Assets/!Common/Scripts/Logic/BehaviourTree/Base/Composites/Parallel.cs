using UnityEngine;
namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Composite/Parallel Single Threaded", -100)]
    public class Parallel : CompositeNode
    {
        public override Status Evaluate(BehaviourTreeController controller)
        {
            bool allCompleted = true;
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                if (child == null)
                {
                    continue;
                }

                var result = child.Evaluate(controller);
                controller.RegisterNodeStatus(child, result);
                allCompleted &= result == Status.Completed;

                if (result == Status.Failed)
                {
                    return Status.Failed;
                }

            }
            return allCompleted ? Status.Completed : Status.Running;
        }
    }
}
