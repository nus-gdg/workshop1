using UnityEngine;

namespace Project.Views.World.Events
{
    [CreateAssetMenu(fileName = "PopCameraLogicAction", menuName = "ScriptableObjects/GameAction/PopCameraLogicAction", order = 1)]
    public class PopCameraLogicAction : GameAction
    {
        public override Status Evaluate(GameContext context, WorldView view)
        {
            view.PopCameraLogic();
            return Status.Success;
        }
    }
}
