using UnityEngine;

namespace Entity
{
    public abstract class PlayerState : ScriptableObject
    {
        public abstract PlayerState Execute(Player player);
    }
}
