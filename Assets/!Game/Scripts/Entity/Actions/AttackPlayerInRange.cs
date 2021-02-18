using Core;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Attack Player In Range", menuName = "State/Movement/Attack Player In Range")]
    public class AttackPlayerInRange : ScriptableObject
    {
        public int range;
        private int _rangeSquared;

        public void Execute(IAttacker attacker)
        {
            var currentPosition = attacker.Transform.position;
            var playerPosition = Game.Instance.World.Player.transform.position;
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
