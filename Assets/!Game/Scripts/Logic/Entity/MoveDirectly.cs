using Common.Logic;
using UnityEngine;

namespace Logic.Entity
{
    [CreateNodeMenu("Entity/Move Directly")]
    public class MoveDirectly : TaskNode
    {
        public BlackboardKey entity;
        public BlackboardKey speed;
        public BlackboardKey direction;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(entity, out Model.Entity entityValue))
            {
                Debug.LogError(entity);
                return Status.Failed;
            }
            if (!controller.TryGetValue(speed, out float speedValue))
            {
                Debug.LogError(speed);
                return Status.Failed;
            }
            if (!controller.TryGetValue(direction, out Vector2 directionValue))
            {
                Debug.LogError(direction);
                return Status.Failed;
            }
            entityValue.Speed = speedValue;
            entityValue.Direction = directionValue;
            return Status.Completed;
        }
    }
}
