using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "PushCameraLogicAction", menuName = "ScriptableObjects/GameAction/PushCameraLogicAction", order = 1)]
public class PushCameraLogicAction : GameAction
{
    public ICameraLogic CameraLogic;
    public override Status Evaluate(GameContext context)
    {
        Assert.IsNotNull(CameraLogic, "PushCameraLogicAction.Evaluate, CameraLogic is null!");
        Common.Game.Instance.World.CameraManager.CameraLogicHandler.PushCameraLogic(CameraLogic);
        return Status.Success;
    }
}
