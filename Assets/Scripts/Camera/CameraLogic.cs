using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CameraController))]
public class CameraLogic : MonoBehaviour
{
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
