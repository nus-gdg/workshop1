using System;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Logic.States.Example
{
    /**
     * Creates a plug and play Example state
     *     fileName = "{YourObject}State"
     *     menuName = "States/{Your Object}"
     */
    [CreateAssetMenu(fileName = "ExampleState", menuName = "States/Example")]
    public class ExampleState : State<Example>
    {
        // --- Fields ---

        // Allows inspector to have plug and play events
        [SerializeField]
        private ExampleEvent events;
        
        // Allows inspector to have plug and play transitions
        [SerializeField]
        private ExampleTransition[] transitions;

        // --- Functions ---
        
        public override void RunEvents(Example controller)
        {
            // Call all listening events
            events?.Invoke(controller);
        }
        
        public override void RunTransitions(Example controller)
        {
            // Check each transition
            for (int i = 0; i < transitions.Length; i++)
            {
                ExampleState nextState;
                
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
         * Custom UnityEvent with Example parameter
         */
        [Serializable]
        private class ExampleEvent : UnityEvent<Example> { }
    }
}
