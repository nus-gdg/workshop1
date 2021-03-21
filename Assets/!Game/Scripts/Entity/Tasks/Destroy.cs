using Common.Logic;
using UnityEngine;

namespace Entity.Tasks
{
    public class Destroy : TaskNode
    {
        [Header("Input")]
        public BlackboardKey objectToDestroy;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (controller.TryGetValue(objectToDestroy, out Transform transformValue))
            {
                Destroy(transformValue.gameObject);
                return Status.Completed;
            }

            if (controller.TryGetValue(objectToDestroy, out Object objectValue))
            {
                Destroy(objectValue);
                return Status.Completed;
            }
            return Status.Failed;
        }
    }

}
