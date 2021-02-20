using System;
using Common.Logic.States;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    /**
     * Creates a plug and play Enemy state
     *     fileName = "{YourObject}State"
     *     menuName = "States/{Your Object}"
     */
    [CreateAssetMenu(fileName = "EnemyState", menuName = "States/Enemy")]
    public class EnemyState : State<Enemy>
    {
        // --- Fields ---

        // Allows inspector to have plug and play events
        [SerializeField]
        private EnemyEvent events;
        
        // Allows inspector to have plug and play transitions
        [SerializeField]
        private EnemyTransition[] transitions;

        // --- Functions ---
        
        public override void RunEvents(Enemy controller)
        {
            // Call all listening events
            events?.Invoke(controller);
        }
        
        public override void RunTransitions(Enemy controller)
        {
            // Check each transition
            for (int i = 0; i < transitions.Length; i++)
            {
                EnemyState nextState;
                
                // Evaluate the transition, and return the next state
                if (transitions[i].condition.IsTrue(controller))
                {
                    nextState = transitions[i].trueState;
                }
                else
                {
                    nextState = transitions[i].falseState;
                }

                // If the state is changed, stop checking the transitions
                if (controller.ChangeState(nextState))
                {
                    break;
                }
            }
        }
        /**
         * Custom UnityEvent with Enemy parameter
         */
        [Serializable]
        private class EnemyEvent : UnityEvent<Enemy> { }
    }
}
