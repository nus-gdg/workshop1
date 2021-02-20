using System;

namespace Common.Logic.States.Example
{
    /**
     * Represents conditional state changes for Example objects
     */
    [Serializable]
    public class ExampleTransition : Transition<ExampleState, ExampleCondition> { }
}
