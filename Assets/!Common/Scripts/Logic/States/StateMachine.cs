using UnityEngine;

namespace Common.Logic.States
{
    /**
     * Base class for any object that requires State handling.
     */
    public abstract class StateMachine<T, TState> : MonoBehaviour
        where TState : State<T>
    {
        // --- Fields ---

        // The current state of the state machine
        [SerializeField] private TState currentState;
        
        // If this is the next state of the state machine, remain in the current state
        [SerializeField] private TState remainState;
        
        // --- Properties ---

        // If you need to check the current state 
        public TState State => currentState;

        // If you need to check how long the current state has been active for
        public float StateTime { get; protected set; }

        // --- Functions ---

        // Performs one update frame for the current state.
        // Call this in the Update loop to run the current state.
        protected void UpdateState(T controller)
        {
            currentState.Execute(controller);
            StateTime += Time.deltaTime;
        }

        // Change the current state of the state machine, and
        // reset the state time.
        public bool ChangeState(TState state)
        {
            // If the next state is remain state,
            // remain in the current state.
            if (remainState == state)
            {
                return false;
            }
            currentState = state;
            StateTime = 0f;
            return true;
        }
    }
}
