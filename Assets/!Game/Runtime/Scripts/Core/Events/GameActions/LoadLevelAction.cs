using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Events
{

    [CreateAssetMenu(fileName = "LoadLevelAction", menuName = "ScriptableObjects/GameAction/LoadLevelAction", order = 1)]
    public class LoadLevelAction : GameAction
    {
        public Progression.Level Level;
        public override GameAction.Status Evaluate(GameContext context)
        {
            return Game.Instance.Progression.RequestLoadLevel(Level) ? GameAction.Status.Success : GameAction.Status.Fail;
        }
    }

}
