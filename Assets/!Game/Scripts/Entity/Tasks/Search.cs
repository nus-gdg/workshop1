using System;
using Common.Logic;
using Core;
using UnityEngine;

namespace Entity.Tasks
{
    [CreateNodeMenu("Enemy/Search")]
    public class Search : TaskNode
    {
        public enum SearchType
        {
            None, Player,
        }
        
        [NodeEnum]
        public SearchType mask;
        public int distance;
        
        [Header("Input")]
        public BlackboardKey transform;
        
        [Header("Output")]
        public BlackboardKey destination;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            switch (mask)
            {
                case SearchType.Player:
                    return FindPlayer(controller);
                default:
                    throw new InvalidOperationException("Current node received unknown search filter");
            }
        }

        public Status FindPlayer(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(transform, out Transform transformValue))
            {
                return Status.Failed;
            }

            var player = Game.Instance.World.Player;
            if (player == null)
            {
                return Status.Failed;
            }

            var playerPosition = player.Transform.position;
            var currentPosition = transformValue.position;

            bool isNearby = (playerPosition - currentPosition).sqrMagnitude < distance * distance;
            if (isNearby)
            {
                controller[destination] = player.Transform;
                return Status.Completed;
            }
            else
            {
                controller[destination] = null;
                return Status.Failed;
            }
        }
    }
}
