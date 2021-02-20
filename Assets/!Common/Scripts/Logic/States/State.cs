using UnityEngine;

namespace Common.Logic.States
{
    /**
     * Base class for any object that has States.
     */
    public abstract class State<T> : ScriptableObject
    {
        // Performs one update frame for the controller
        public void Execute(T controller)
        {
            RunEvents(controller);
            RunTransitions(controller);
        }

        // Handles the movement and behavior of the object
        public abstract void RunEvents(T controller);

        // Handles the changing of states
        public abstract void RunTransitions(T controller);

        public virtual void Enter(T controller) { }
        public virtual void Exit(T controller) { }
        
    }
}
