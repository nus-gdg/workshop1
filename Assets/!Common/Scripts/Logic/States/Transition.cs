namespace Common.Logic.States
{
    public abstract class Transition<TState, TCondition>
    {
        // Determines the next state of a state machine
        public TCondition condition;

        // The next state if the condition is true
        public TState trueState;

        // The next state if the condition is false
        public TState falseState;
    }
}
