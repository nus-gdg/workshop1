using Common.Logic;
using UnityEngine;

namespace Entity.Tasks
{
    [CreateNodeMenu("Enemy/Boss/Kraken")]
    public class KrakenNode : TaskNode
    {
        [Header("Input")]
        public BlackboardKey Kraken;
        public Command KrakenCommand;
        public enum Command
        {
            Dive,
            Emerge,
            TentacleDive,
            TentacleEmerge,
            TentacleMoveToRandomPosition,
            TentacleMoveToZoneBoundary,
        }

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(Kraken, out Kraken kraken))
            {
                return Status.Failed;
            }
            switch (KrakenCommand)
            {
                case Command.Dive:
                    kraken.Dive();
                    break;
                case Command.Emerge:
                    kraken.Emerge();
                    break;
                case Command.TentacleDive:
                    kraken.TentacleDive();
                    break;
                case Command.TentacleEmerge:
                    kraken.TentacleEmerge();
                    break;
                case Command.TentacleMoveToRandomPosition:
                    kraken.TentacleMoveToRandomPosition();
                    break;
                case Command.TentacleMoveToZoneBoundary:
                    kraken.TentacleMoveToZoneBoundary();
                    break;
            }
            return Status.Completed;
        }
    }
}