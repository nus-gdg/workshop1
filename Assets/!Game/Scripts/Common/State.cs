namespace Project.Common
{
    public interface IState
    {
        float DecisionUpdateRate { get; }
        bool StateEnded { get; }

        void OnEnter();
        void OnExit();

        void Tick();
    }
}
