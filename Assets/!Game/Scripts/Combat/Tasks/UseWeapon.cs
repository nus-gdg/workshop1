using Common.Logic;
using UnityEngine;
using Combat.Weapons;

namespace Combat.Tasks
{
    [CreateNodeMenu("Entity/Combat/UseWeapon")]
    public class UseWeapon : TaskNode
    {
        [Header("Input")]
        public BlackboardKey Weapon;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(Weapon, out Weapon weapon))
            {
                return Status.Failed;
            }
            weapon.Attack();
            return Status.Completed;
        }
    }
}