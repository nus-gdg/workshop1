using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Gives instructions to a CameraController component \n
/// on where its target location should be. 
/// </summary>
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CameraController))]
public class CameraLogic : MonoBehaviour
{
    /* TODO:
     * If we are implementing multiple camera modes, we may be 
     * implementing an ICameraLogic interface. We may have to 
     * think about how to structure the codebase if we are going 
     * to implement camera shaking.
     * 
     * For now, do not reference CameraController or CameraLogic 
     * through an external script. Instead, create an API for public 
     * users to perform fancy camera actions (if necessary). 
     */

    /// <summary>
    /// The extent that the cursor's position will influence the camera's position. \n
    /// More specifically, cameraPos = x * playerPos + (1-x) * cursorPos
    /// </summary>
    [SerializeField]
    [Range(0.0f, 0.5f)]
    private float PlayerBias = 0.25f;

    private CameraController cameraController;
    private Camera attachedCamera;

    void Awake()
    {
        attachedCamera = GetComponent<Camera>();
        Assert.IsNotNull(attachedCamera, "CameraController expects an attached camera");
        cameraController = GetComponent<CameraController>();
        Assert.IsNotNull(cameraController, "CameraController expects a CameraController");
    }

    void LateUpdate()
    {
        Entity.Player player = Common.Game.Instance.World.Player;
        Assert.IsNotNull(player, "CameraLogic.LateUpdate player registered in world is null");

        Entity.Cursor cursor = Common.Game.Instance.World.Cursor;
        Assert.IsNotNull(cursor, "CameraLogic.LateUpdate cursor registered in world is null");

        Vector3 targetPosition = Vector3.Lerp(player.transform.position, cursor.transform.position, PlayerBias);
        cameraController.TargetPosition = targetPosition;
    }
}
