using Combat.Weapons;
using Common.Logic;
using UnityEngine;

namespace Logic.Combat
{
    [CreateNodeMenu("Combat/Shoot Bullet")]
    public class ShootBullet : TaskNode
    {
        public BlackboardKey weapon;
        public Rigidbody2D prefab;
        public float force;
        public float angleOffset;
        
        //TODO: add offset

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(weapon, out Weapon weaponValue))
            {
                Debug.Log(weapon);
                return Status.Failed;
            }
            if (prefab == null)
            {
                Debug.Log("Prefab");
                return Status.Failed;
            }

            Transform firepoint = weaponValue.FirePoint;
            Quaternion rotation = firepoint.rotation * Quaternion.Euler(0, 0, angleOffset);
            Rigidbody2D rigidbody = Instantiate(prefab, firepoint.position, rotation);
            rigidbody.AddForce(force * rigidbody.transform.up, ForceMode2D.Impulse);
            return Status.Completed;
        }
    }
}
