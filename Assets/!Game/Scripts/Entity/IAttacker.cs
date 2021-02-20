using Combat.Weapons;
using UnityEngine;

namespace Entity
{
    public interface IAttacker
    {
        Transform Transform { get; }
        Weapon Weapon { get; set; }
    }
}
