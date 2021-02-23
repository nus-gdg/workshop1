using System;

namespace Common.Logic
{
    [CreateNodeMenu("Composite/Sequence", -100)]
    public class Sequence : CompositeNode
    {
        public override Status Evaluate(BehaviorTreeController controller)
        {
            for (int i = GetStartingNodeIndex(controller); i < children.Count; i++)
            {
                var child = children[i];
                if (child == null)
                {
                    continue;
                }

                var result = child.Evaluate(controller);
                controller.RegisterNodeStatus(child, result);

                if (result != Status.Completed)
                {
                    return result;
                }
            }
            return Status.Completed;
        }
        
        private int GetStartingNodeIndex(BehaviorTreeController controller)
        {
            if (!controller.IsRunningNode(this))
            {
                return 0;
            }
            for (int i = 0; i < children.Count; i++)
            {
                if (controller.IsRunningNode(children[i]))
                {
                    return i;
                }
            }
            throw new InvalidOperationException("Running node should have a running child");
        }
    }
}
