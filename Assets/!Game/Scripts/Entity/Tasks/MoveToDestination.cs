using Common.Logic;
using Pathfinding;
using UnityEngine;

namespace Entity.Tasks
{
    [CreateNodeMenu("Enemy/Move To Destination")]
    public class MoveToDestination : TaskNode
    {
        [Header("Input")]
        public BlackboardKey ai;
        public BlackboardKey destination;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(ai, out AIDestinationSetter aiValue)
            || !controller.TryGetValue(destination, out Transform destinationValue))
            {
                return Status.Failed;
            }
            aiValue.target = destinationValue;
            return Status.Completed;
        }
    }
}
