using Project.Views.Controllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Views.World.Events
{
    [CreateAssetMenu(fileName = "PushCameraLogicAction", menuName = "ScriptableObjects/GameAction/PushCameraLogicAction", order = 1)]
    public class PushCameraLogicAction : GameAction
    {
        public CameraLogic CameraLogic;

        public override Status Evaluate(GameContext context, WorldView view)
        {
            Assert.IsNotNull(CameraLogic, "PushCameraLogicAction.Evaluate, CameraLogic is null!");
            view.PushCameraLogic(CameraLogic);
            return Status.Success;
        }
    }
}
