using Core;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "InputAim", menuName = "Events/Input/Aim")]
    public class InputAimEvent : ScriptableObject
    {
        public void Execute(IInputPlayerAttackListener listener)
        {
            listener.Weapon.Aim(Game.Instance.World.Cursor.transform.position);
        }
    }
}
