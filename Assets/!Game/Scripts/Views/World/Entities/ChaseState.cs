using Pathfinding;
using Project.Common;

namespace Project.Views.World.Entities
{
    public class ChaseState : IState
    {
        private WorldView _view;
        private IAstarAI _ai;

        public float DecisionUpdateRate { get; private set; } = 0.1f;
        public bool StateEnded { get; private set; } = false;

        public ChaseState(WorldView view, IAstarAI ai)
        {
            _view = view;
            _ai = ai;
        }

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
            _ai.destination = _view.PlayerPosition;
        }
    }
}
