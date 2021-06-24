using System;
using UnityEngine;

namespace Project.Views.World.Entities
{
    [Serializable]
    public class PlayerDashState : PlayerState
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float timeInState;

        [SerializeField]
        private float maxTimeInState;

        public PlayerDashState()
        {
            name = "Player Dash";
            speed = 16f;
            timeInState = 0f;
            maxTimeInState = 0.2f;
        }

        public override void Enter(PlayerUi player)
        {
            player.PlayAnimation("Blend Tree (Walk)");
            player.Speed = speed;
        }

        public override void Update(PlayerUi player)
        {
            player.UpdateWeapon();
            player.UpdateInput();

            if (timeInState > maxTimeInState)
            {
                player.ChangeState(new PlayerWalkState());
                return;
            }
            timeInState += Time.deltaTime;
        }

        public override void Exit(PlayerUi player)
        {

        }
    }
}
