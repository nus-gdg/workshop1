using Project.Models.Levels;
using UnityEngine;

namespace Project.Views.World.Events
{
    [CreateAssetMenu(fileName = "LoadLevelAction", menuName = "ScriptableObjects/GameAction/LoadLevelAction", order = 1)]
    public class LoadLevelAction : GameAction
    {
        public Level Level;

        public override Status Evaluate(GameContext context, WorldView view)
        {
            return view.RequestLoadLevel(Level) ? Status.Success : Status.Fail;
        }
    }
}
