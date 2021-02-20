using Core;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "AimAtPlayer", menuName = "Events/Ai/Movement/Aim At Player")]
    public class AimAtPlayerEvent : ScriptableObject
    {
        public void Execute(IAttacker attacker)
        {
            attacker.Weapon.Aim(Game.Instance.World.Player.Transform.position);
        }
    }
}
