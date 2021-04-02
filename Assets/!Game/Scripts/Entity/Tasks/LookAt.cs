using Common.Logic;
using UnityEngine;

namespace Entity.Tasks
{
    [CreateNodeMenu("Entity/Look At 2D")]
    public class LookAt : TaskNode
    {
        [Header("Input")]
        public BlackboardKey transform;
        public BlackboardKey targetTransform;
        public float RotationSpeedDegrees = 1.0f;
        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(transform, out Transform transformValue)
            || !controller.TryGetValue(targetTransform, out Transform targetValue))
            {
                return Status.Failed;
            }

            Vector3 lookAtUp = (targetValue.position - transformValue.position).normalized;
            Vector3 targetUp = Vector3.MoveTowards(transformValue.up, lookAtUp, Mathf.Deg2Rad * RotationSpeedDegrees * Time.deltaTime);
            transformValue.up = targetUp;

            // aproximately equal
            return transformValue.up == lookAtUp ? Status.Completed : Status.Running;
        }
    }
}
