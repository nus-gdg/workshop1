using UnityEngine.Events;

namespace Core.Events
{
    [System.Serializable]
    public class GameEvent : UnityEvent<GameContext>
    {
    }
}