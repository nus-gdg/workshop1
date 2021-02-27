using Common.Logic;
using UnityEngine;

namespace Entity.Tasks
{
    [CreateNodeMenu("Enemy/Boss/Kraken")]
    public class KrakenNode : TaskNode
    {
        [Header("Input")]
        public BlackboardKey Kraken;
        public State KrakenState;
        public enum State
        {
            Dive,
            Emerge,
        }

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(Kraken, out Kraken kraken))
            {
                return Status.Failed;
            }
            switch (KrakenState)
            {
                case State.Dive:
                    kraken.Dive();
                    break;
                case State.Emerge:
                    kraken.Emerge();
                    break;
            }
            return Status.Completed;
        }
    }
}