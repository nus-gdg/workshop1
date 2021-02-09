using UnityEngine;

[CreateAssetMenu(fileName = "PopCameraLogicAction", menuName = "ScriptableObjects/GameAction/PopCameraLogicAction", order = 1)]
public class PopCameraLogicAction : GameAction
{
    public CameraLogic CameraLogic;
    public override Status Evaluate(GameContext context)
    {
        Common.Game.Instance.World.Camera.PopCameraLogic(CameraLogic);
        return Status.Success;
    }
}
