using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CameraController))]
public class CameraLogic : MonoBehaviour
{
    public Transform PlayerTransform;
    public Transform Cursor;
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
        Vector3 targetPosition = PlayerTransform.position * 0.75f + Cursor.position * 0.25f;
        cameraController.TargetPosition = targetPosition;
    }
}
