using Common.Logic;
using UnityEngine;

namespace Entity.Tasks
{
    [CreateNodeMenu("Entity/Set GameObject Active")]
    public class SetActive : TaskNode
    {
        [Header("Input")]
        public BlackboardKey gameObject;
        public bool Active = true;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(gameObject, out GameObject gameObjValue))
            {
                return Status.Failed;
            }

            gameObjValue.SetActive(Active);
            return Status.Completed;
        }
    }
}