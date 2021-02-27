using Combat.Weapons;
using Common.Logic;
using UnityEngine;

namespace Logic.Combat
{
    [CreateNodeMenu("Combat/Shoot Bullet Ring")]
    public class ShootRing : TaskNode
    {
        public BlackboardKey weapon;
        public Rigidbody2D prefab;
        public float force;
        public float size;

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
            var offset = firepoint.rotation.eulerAngles.z;
            var angleInterval = 360f / size;

            for (float angle = offset; angle < 360 + offset; angle += angleInterval)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Rigidbody2D rigidbody = Instantiate(prefab, firepoint.position, rotation);
                rigidbody.AddForce(force * rigidbody.transform.up, ForceMode2D.Impulse);
            }
            return Status.Completed;
        }
    }
}
