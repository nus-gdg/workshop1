using Core.Levels;
using UnityEngine;

namespace Core.Events
{

    [CreateAssetMenu(fileName = "LoadLevelAction", menuName = "ScriptableObjects/GameAction/LoadLevelAction", order = 1)]
    public class LoadLevelAction : GameAction
    {
        public Level Level;
        public override GameAction.Status Evaluate(GameContext context)
        {
            return Game.Instance.Levels.RequestLoadLevel(Level) ? GameAction.Status.Success : GameAction.Status.Fail;
        }
    }
}
