using UnityEngine;

namespace Entity
{
    /**
     * Creates a plug and play Player condition
     *     fileName = "ConditionName"
     *     menuName = "Conditions/{Your Object}/{Condition Name}"
     */
    [CreateAssetMenu(fileName = "IfPlayerStateTimeout", menuName = "Conditions/Player/State Timeout")]
    public class PlayerStateTimeout : PlayerCondition
    {
        public float time;

        public override bool IsTrue(Player controller)
        {
            // Returns true if the state machine has been in the current state
            // for a specific amount of time.
            return controller.StateTime > time;
        }
    }
}
