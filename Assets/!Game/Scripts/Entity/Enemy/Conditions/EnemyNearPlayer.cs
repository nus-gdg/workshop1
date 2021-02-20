using Core;
using UnityEngine;

namespace Entity
{
    /**
     * Creates a plug and play Enemy condition
     *     fileName = "ConditionName"
     *     menuName = "Conditions/{Your Object}/{Condition Name}"
     */
    [CreateAssetMenu(fileName = "IfEnemyNearPlayer", menuName = "Conditions/Enemy/Near Player")]
    public class EnemyNearPlayer : EnemyCondition
    {
        public float distance;

        public override bool IsTrue(Enemy controller)
        {
            var currentPosition = controller.transform.position;
            var playerPosition = Game.Instance.World.Player.transform.position;

            // Square Magnitude is faster than Magnitude
            // As it does not have to normalize the vector (division is slow)
            var sqrDistanceFromPlayer = (playerPosition - currentPosition).sqrMagnitude;
            
            // Returns true if the enemy is near the player.
            return sqrDistanceFromPlayer < distance * distance;
        }
    }
}
