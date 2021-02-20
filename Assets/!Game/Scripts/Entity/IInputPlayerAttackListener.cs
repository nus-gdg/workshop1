using Combat.Weapons;
using Core.Managers;

namespace Entity
{
    public interface IInputPlayerAttackListener
    {
        InputManager.PlayerActions Controls { get; }
        Weapon Weapon { get; }
    }
}
