using Core;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "AttackNearbyPlayer", menuName = "Events/Ai/Attack/Nearby Player")]
    public class AttackNearbyPlayerEvent : ScriptableObject
    {
        public int range;
        private int _rangeSquared;

        public void Execute(IAttacker attacker)
        {
            var currentPosition = attacker.Transform.position;
            var playerPosition = Game.Instance.World.Player.Transform.position;
            var distance = (playerPosition - currentPosition).sqrMagnitude;

            attacker.Weapon.Aim(playerPosition);
            if (distance < _rangeSquared)
            {
                attacker.Weapon.Attack();
            }
        }

        private void OnValidate()
        {
            _rangeSquared = range * range;
        }
    }
    
    // TODO: Custom inspector for range squared
}
