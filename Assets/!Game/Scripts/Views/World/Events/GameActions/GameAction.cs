using UnityEngine;

namespace Project.Views.World.Events
{
    public abstract class GameAction : ScriptableObject
    {
        public enum Status
        {
            Success,
            Fail,
            Invalid
        }
        public void EvaluateContext(GameContext context, WorldView view) { Evaluate(context, view); }
        public abstract Status Evaluate(GameContext context, WorldView view);
    }
}
