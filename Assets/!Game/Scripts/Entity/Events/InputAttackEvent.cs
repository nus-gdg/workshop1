using UnityEngine;

namespace Entity.Events
{
    [CreateAssetMenu(fileName = "InputAttack", menuName = "Events/Input/Attack")]
    public class InputAttackEvent : ScriptableObject
    {
        public void Execute(IInputPlayerAttackListener listener)
        {
            if (listener.Controls.Action1.ReadValue<float>() > 0f)
            {
                listener.Weapon.Attack();
            }
        }
    }
}
