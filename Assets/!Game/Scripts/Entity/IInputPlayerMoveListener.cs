using Core.Managers;
using UnityEngine;

namespace Entity
{
    public interface IInputPlayerMoveListener
    {
        InputManager.PlayerActions Controls { get; }
        Vector2 Direction { get; set; }
        float Speed { get; set; }
    }
}
