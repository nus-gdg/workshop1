using Common.Logic;
using UnityEngine;

namespace Entity.Tasks
{
    [CreateNodeMenu("Entity/Enable")]
    public class Enable : TaskNode
    {
        [Header("Input")]
        public BlackboardKey behaviour;
        public bool Enabled = true;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(behaviour, out Behaviour behaviourValue))
            {
                return Status.Failed;
            }

            behaviourValue.enabled = Enabled;
            return Status.Completed;
        }
    }
}
