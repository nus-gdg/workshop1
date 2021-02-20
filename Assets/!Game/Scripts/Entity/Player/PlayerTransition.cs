using System;
using Common.Logic.States;

namespace Entity
{
    /**
     * Represents conditional state changes for Player objects
     */
    [Serializable]
    public class PlayerTransition : Transition<PlayerState, PlayerCondition> { }
}
