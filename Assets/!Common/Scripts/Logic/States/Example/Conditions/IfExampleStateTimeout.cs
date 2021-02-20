using UnityEngine;

namespace Common.Logic.States.Example
{
    /**
     * Creates a plug and play Example condition
     *     fileName = "ConditionName"
     *     menuName = "Conditions/{Your Object}/{Condition Name}"
     */
    [CreateAssetMenu(fileName = "IfExampleStateTimeout", menuName = "Conditions/Example/State Timeout")]
    public class IfExampleStateTimeout : ExampleCondition
    {
        public float time;

        public override bool IsTrue(Example controller)
        {
            // Returns true if the state machine has been in the current state
            // for a specific amount of time.
            return controller.StateTime > time;
        }
    }
}
