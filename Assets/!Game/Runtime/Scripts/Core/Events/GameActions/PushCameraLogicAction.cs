using UnityEngine;
using UnityEngine.Assertions;
using World.Camera;

namespace Core.Events
{
    [CreateAssetMenu(fileName = "PushCameraLogicAction", menuName = "ScriptableObjects/GameAction/PushCameraLogicAction", order = 1)]
    public class PushCameraLogicAction : GameAction
    {
        public CameraLogic CameraLogic;
        public override Status Evaluate(GameContext context)
        {
            Assert.IsNotNull(CameraLogic, "PushCameraLogicAction.Evaluate, CameraLogic is null!");
            Core.Game.Instance.World.Camera.PushCameraLogic(CameraLogic);
            return Status.Success;
        }
    }
}