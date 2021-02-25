using Common.Logic;
using UnityEngine;

namespace Logic.Entity
{
    [CreateNodeMenu("Entity/Instantiate Object")]
    public class InstantiateObject : TaskNode
    {
        public BlackboardKey key;

        [Header("Settings")]
        public BlackboardKey prefab;
        public BlackboardKey origin;
        public BlackboardKey rotation;
        public BlackboardKey parentTransform;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(prefab, out Model.Entity prefabValue))
            {
                Debug.Log(prefab);
                return Status.Failed;
            }
            if (!controller.TryGetValue(origin, out Vector2 originValue))
            {
                Debug.Log(position);
                return Status.Failed;
            }
            if (!controller.TryGetValue(rotation, out Vector2 rotationValue))
            {
                Debug.Log(rotation);
                return Status.Failed;
            }
            if (!controller.TryGetValue(parentTransform, out Transform parentTransformValue))
            {
                Debug.Log(parent);
                return Status.Failed;
            }

            // GameObject.Instantiate(prefabValue, originValue, rotationValue, parentTransformValue);
            return Status.Completed;
        }
    }
}
