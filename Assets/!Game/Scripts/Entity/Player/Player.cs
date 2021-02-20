using Common.Logic.States;

namespace Entity
{
    /**
     * Custom StateMachine{Player, PlayerState}
     *     Player = This class, which inherits from StateMachine
     *     PlayerState = The state class, which is a State{Player}
     */
    public class Player : StateMachine<Player, PlayerState>
    {
        // Always update state in the Update loop.
        // Note that we can also not update the state if the game is paused
        private void Update()
        {
            UpdateState(this);
        }
    }
}
