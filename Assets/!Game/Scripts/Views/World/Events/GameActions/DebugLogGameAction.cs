using UnityEngine;

namespace Project.Views.World.Events
{
    [CreateAssetMenu(fileName = "DebugLogGameAction", menuName = "ScriptableObjects/GameAction/DebugLogGameAction", order = 1)]
    public class DebugLogGameAction : GameAction
    {
        public string DebugText = "";

        public override Status Evaluate(GameContext context, WorldView view)
        {
            Debug.Log(DebugText);
            return Status.Success;
        }
    }
}
