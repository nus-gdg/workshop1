using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Enemy Walk", menuName = "State/Enemy/Walk")]
    public class EnemyWalkState : EnemyState
    {
        public EnemyEvent onWalk;

        public override EnemyState Execute(Enemy enemy)
        {
            onWalk?.Invoke(enemy);

            // var currentPosition = enemy.Transform.position;
            // var playerPosition = Game.Instance.World.Player.transform.position;
            // var distance = (playerPosition - currentPosition).sqrMagnitude;
            //
            // enemy.Weapon.Aim(playerPosition);
            // if (distance < 25)
            // {
            //     onAttack?.Invoke(enemy.Weapon);
            // }

            return this;
        }
    }
}
