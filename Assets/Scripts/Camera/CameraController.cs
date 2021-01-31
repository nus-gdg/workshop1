using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



/// <summary>
/// Handles the low-level controls of the camera. For now, \n
/// it only controls the camera's position. 
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [System.Serializable]
    public struct ControllerProperties
    {
        public Vector3 TargetPosition;
        public float SmoothTime;
        public float TargetOrthographicSize;
        public float ZoomSmoothTime;
    }

    [HideInInspector]
    public ControllerProperties CurrentProperties;
    public ControllerProperties DefaultProperties;

    private Vector3 velocity = Vector3.zero;
    private float zoomSpeed = 0.0f;

    private Camera attachedCamera;

    void Awake()
    {
        attachedCamera = GetComponent<Camera>();
        Assert.IsNotNull(attachedCamera, "CameraController expects an attached camera");
        Assert.IsTrue(attachedCamera.orthographic, "CameraController expects an orthographic camera");
        CurrentProperties = DefaultProperties;
    }

    void LateUpdate()
    {
        CurrentProperties.TargetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, CurrentProperties.TargetPosition, ref velocity, CurrentProperties.SmoothTime);
        attachedCamera.orthographicSize = Mathf.SmoothDamp(attachedCamera.orthographicSize, CurrentProperties.TargetOrthographicSize, ref zoomSpeed, CurrentProperties.ZoomSmoothTime);
    }
}
