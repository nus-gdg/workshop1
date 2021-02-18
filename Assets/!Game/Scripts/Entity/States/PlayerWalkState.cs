using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Player Walk", menuName = "State/Player/Walk")]
    public class PlayerWalkState : PlayerState
    {
        public PlayerEvent onWalk;

        public override PlayerState Execute(Player player)
        {
            onWalk?.Invoke(player);
            return this;
        }
    }
}
