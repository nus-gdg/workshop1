using Core;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Aim At Cursor", menuName = "State/Movement/Aim At Cursor")]
    public class AimAtCursor : ScriptableObject
    {
        public void Execute(IAttacker attacker)
        {
            attacker.Weapon.Aim(Game.Instance.World.Cursor.transform.position);
        }
    }
}
