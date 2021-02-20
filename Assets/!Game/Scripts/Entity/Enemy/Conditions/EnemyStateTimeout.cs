using UnityEngine;

namespace Entity
{
    /**
     * Creates a plug and play Enemy condition
     *     fileName = "ConditionName"
     *     menuName = "Conditions/{Your Object}/{Condition Name}"
     */
    [CreateAssetMenu(fileName = "IfEnemyStateTimeout", menuName = "Conditions/Enemy/State Timeout")]
    public class EnemyStateTimeout : EnemyCondition
    {
        public float time;

        public override bool IsTrue(Enemy controller)
        {
            // Returns true if the state machine has been in the current state
            // for a specific amount of time.
            return controller.StateTime > time;
        }
    }
}
