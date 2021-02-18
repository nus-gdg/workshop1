using Core;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Follow Player Movement", menuName = "State/Movement/Follow Player")]
    public class FollowPlayerMovement : ScriptableObject
    {
        public void Execute(IPathfinder pathfinder)
        {
            pathfinder.Ai.destination = Game.Instance.World.Player.transform.position;
        }
    }
}
