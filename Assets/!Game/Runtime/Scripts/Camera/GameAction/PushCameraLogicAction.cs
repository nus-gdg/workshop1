using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "PushCameraLogicAction", menuName = "ScriptableObjects/GameAction/PushCameraLogicAction", order = 1)]
public class PushCameraLogicAction : GameAction
{
    public CameraLogic CameraLogic;
    public override Status Evaluate(GameContext context)
    {
        Assert.IsNotNull(CameraLogic, "PushCameraLogicAction.Evaluate, CameraLogic is null!");
        Common.Game.Instance.World.Camera.PushCameraLogic(CameraLogic);
        return Status.Success;
    }
}
