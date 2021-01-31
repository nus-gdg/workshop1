using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "PlayerAndCursorCameraLogic", menuName = "ScriptableObjects/CameraLogic/PlayerAndCursorCameraLogic", order = 1)]
public class PlayerAndCursorCameraLogic : ICameraLogic
{
    /// <summary>
    /// The extent that the cursor's position will influence the camera's position. \n
    /// More specifically, cameraPos = x * playerPos + (1-x) * cursorPos
    /// </summary>
    [SerializeField]
    [Range(0.0f, 0.5f)]
    private float PlayerBias = 0.25f;
    public override void OnLateUpdate(CameraController controller)
    {
        Entity.Player player = Common.Game.Instance.World.Player;
        Assert.IsNotNull(player, "CameraLogic.LateUpdate player registered in world is null");

        Entity.Cursor cursor = Common.Game.Instance.World.Cursor;
        Assert.IsNotNull(cursor, "CameraLogic.LateUpdate cursor registered in world is null");

        Vector3 targetPosition = Vector3.Lerp(player.transform.position, cursor.transform.position, PlayerBias);
        controller.CurrentProperties.TargetPosition = targetPosition;
    }
}
