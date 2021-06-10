using UnityEngine;

namespace Project.Views.World.Entities
{
    public class PlayerIdleState : PlayerState
    {
        private bool _allowDash;

        public PlayerIdleState(bool allowDash)
        {
            name = "Player Idle";
            _allowDash = allowDash;
        }

        public override void Enter(PlayerUi player)
        {
            player.PlayAnimation("Blend Tree (Idle)");
            player.Speed = 0f;
        }

        public override void Update(PlayerUi player)
        {
            player.UpdateMovement();
            player.UpdateWeapon();
            player.UpdateInput();

            if (!player.IsMoving)
            {
                return;
            }

            if (_allowDash)
            {
                if ((player.input.left.HasDoubleTap() && player.Direction == Vector3.left)
                    || (player.input.right.HasDoubleTap() && player.Direction == Vector3.right)
                    || (player.input.up.HasDoubleTap() && player.Direction == Vector3.up)
                    || (player.input.down.HasDoubleTap() && player.Direction == Vector3.down))
                {
                    player.ChangeState(new PlayerDashState());
                    return;
                }
            }

            player.ChangeState(new PlayerWalkState());
        }

        public override void Exit(PlayerUi player)
        {

        }
    }
}
