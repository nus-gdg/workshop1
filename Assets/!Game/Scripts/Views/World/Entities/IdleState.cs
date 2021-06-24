using Project.Common;

namespace Project.Views.World.Entities
{
    public class IdleState : IState
    {
        public float DecisionUpdateRate { get; private set; } = 0.1f;
        public bool StateEnded { get; private set; } = false;

        public void OnEnter()
        {
            //
        }

        public void OnExit()
        {
            //
        }

        public void Tick()
        {
            // Do nothing
        }
    }
}
