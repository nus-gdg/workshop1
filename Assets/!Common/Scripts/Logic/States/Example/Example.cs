namespace Common.Logic.States.Example
{
    /**
     * Custom StateMachine{Example, ExampleState}
     *     Example = This class, which inherits from StateMachine
     *     ExampleState = The state class, which is a State{Example}
     */
    public class Example : StateMachine<Example, ExampleState>
    {
        // Always update state in the Update loop.
        // Note that we can also not update the state if the game is paused
        private void Update()
        {
            UpdateState(this);
        }
    }
}
