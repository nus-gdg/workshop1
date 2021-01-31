using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopCameraLogicAction", menuName = "ScriptableObjects/GameAction/PopCameraLogicAction", order = 1)]
public class PopCameraLogicAction : GameAction
{
    public ICameraLogic CameraLogic;
    public override Status Evaluate(GameContext context)
    {
        Common.Game.Instance.World.CameraManager.CameraLogicHandler.PopCameraLogic(CameraLogic);
        return Status.Success;
    }
}
