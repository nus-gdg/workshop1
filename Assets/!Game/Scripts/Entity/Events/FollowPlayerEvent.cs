using Core;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "FollowPlayer", menuName = "Events/Ai/Movement/Follow Player")]
    public class FollowPlayerEvent : ScriptableObject
    {
        public void Execute(IPathfinder pathfinder)
        {
            pathfinder.Ai.destination = Game.Instance.World.Player.Transform.position;
        }
    }
}
