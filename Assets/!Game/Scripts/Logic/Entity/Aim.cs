using Combat.Weapons;
using Common.Logic;
using UnityEngine;

namespace Logic.Entity
{
    [CreateNodeMenu("Entity/Aim")]
    public class Aim : TaskNode
    {
        public BlackboardKey weapon;
        public BlackboardKey target;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(weapon, out Weapon weaponValue))
            {
                return Status.Failed;
            }
            if (!controller.TryGetValue(target, out Vector2 targetValue))
            {
                return Status.Failed;
            }
            weaponValue.Aim(targetValue);
            return Status.Completed;
        }
    }
}
