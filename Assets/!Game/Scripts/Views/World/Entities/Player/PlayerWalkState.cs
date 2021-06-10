using System;
using UnityEngine;

namespace Project.Views.World.Entities
{
    [Serializable]
    public class PlayerWalkState : PlayerState
    {
        [SerializeField]
        private float speed;

        public PlayerWalkState()
        {
            name = "Player Walk";
            speed = 4f;
        }

        public override void Enter(PlayerUi player)
        {
            player.PlayAnimation("Blend Tree (Walk)");
            player.Speed = speed;
        }

        public override void Update(PlayerUi player)
        {
            player.UpdateMovement();
            player.UpdateWeapon();
            player.UpdateInput();

            if (!player.IsMoving)
            {
                player.ChangeState(new PlayerIdleState(true));
                return;
            }
        }

        public override void Exit(PlayerUi player)
        {

        }
    }
}
