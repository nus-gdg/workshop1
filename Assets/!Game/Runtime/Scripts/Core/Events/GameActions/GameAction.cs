using UnityEngine;

namespace Core.Events
{
    public abstract class GameAction : ScriptableObject
    {
        public enum Status
        {
            Success,
            Fail,
            Invalid
        }
        public void EvaluateContext(GameContext context) { Evaluate(context); }
        public abstract Status Evaluate(GameContext context);
    }
}