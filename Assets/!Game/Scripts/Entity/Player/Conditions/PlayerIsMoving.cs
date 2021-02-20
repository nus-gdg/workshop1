using UnityEngine;

namespace Entity
{
    /**
     * Creates a plug and play Player condition
     *     fileName = "ConditionName"
     *     menuName = "Conditions/{Your Object}/{Condition Name}"
     */
    [CreateAssetMenu(fileName = "IfPlayerIsMoving", menuName = "Conditions/Player/Is Moving")]
    public class PlayerIsMoving : PlayerCondition
    {
        public override bool IsTrue(Player controller)
        {
            // Returns true if the player is moving
            return controller.Direction != Vector2.zero;
        }
    }
}
