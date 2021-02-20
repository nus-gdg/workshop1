using System;
using Common.Logic.States;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    /**
     * Creates a plug and play Player state
     *     fileName = "{YourObject}State"
     *     menuName = "States/{Your Object}"
     */
    [CreateAssetMenu(fileName = "PlayerState", menuName = "States/Player")]
    public class PlayerState : State<Player>
    {
        // --- Fields ---

        // Allows inspector to have plug and play events
        [SerializeField]
        private PlayerEvent events;
        
        // Allows inspector to have plug and play transitions
        [SerializeField]
        private PlayerTransition[] transitions;

        // --- Functions ---
        
        public override void RunEvents(Player controller)
        {
            // Call all listening events
            events?.Invoke(controller);
        }
        
        public override void RunTransitions(Player controller)
        {
            // Check each transition
            for (int i = 0; i < transitions.Length; i++)
            {
                PlayerState nextState;
                
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
         * Custom UnityEvent with Player parameter
         */
        [Serializable]
        private class PlayerEvent : UnityEvent<Player> { }
    }
}
