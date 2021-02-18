using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Controller Attack", menuName = "Actions/Player/Controller Attack")]
    public class ControllerAttack : ScriptableObject
    {
        public void Execute(Player player)
        {
            if (player.Controls.Action1.ReadValue<float>() > 0f)
            {
                player.Weapon.Attack();
            }
        }
    }
}
