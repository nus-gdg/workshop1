using Common.Logic.States;

namespace Entity
{
    /**
     * Custom StateMachine{Enemy, EnemyState}
     *     Enemy = This class, which inherits from StateMachine
     *     EnemyState = The state class, which is a State{Enemy}
     */
    public class Enemy : StateMachine<Enemy, EnemyState>
    {
        // Always update state in the Update loop.
        // Note that we can also not update the state if the game is paused
        private void Update()
        {
            UpdateState(this);
        }
    }
}
