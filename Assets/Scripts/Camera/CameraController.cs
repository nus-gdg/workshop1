using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public CameraLogic BaseCameraLogic;

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

    private Camera attachedCamera;
    private Vector3 velocity = Vector3.zero;
    private float zoomSpeed = 0.0f;

    private class CameraLogicAndControllerPropertyPair
    {
        public CameraLogic CameraLogic;
        public CameraController.ControllerProperties PreviousLogicProperties;
    }
    private Stack<CameraLogicAndControllerPropertyPair> CameraLogicStack;
 
    /// <summary>
    /// Pushes a new camera logic
    /// </summary>
    /// <param name="cameraLogic">Camera Logic to push</param>
    public void PushCameraLogic(CameraLogic cameraLogic)
    {
        CameraLogicAndControllerPropertyPair logicAndControllerPropertyPair = new CameraLogicAndControllerPropertyPair();
        logicAndControllerPropertyPair.CameraLogic = cameraLogic;
        logicAndControllerPropertyPair.PreviousLogicProperties = CurrentProperties; // copy
        CameraLogicStack.Push(logicAndControllerPropertyPair);
        cameraLogic.OnPush(this);
    }

    /// <summary>
    /// Pops camera logic off the stack. If you provide the camera logic, will do runtime check if cameralogic was indeed on top of stack
    /// </summary>
    /// <param name="cameraLogic">Camera Logic to check</param>
    public void PopCameraLogic(CameraLogic cameraLogic = null)
    {
        CameraLogicAndControllerPropertyPair logicAndControllerPropertyPair = CameraLogicStack.Pop();
        Assert.IsNotNull(logicAndControllerPropertyPair, "CameraLogicHandler.PopCameraLogic top of queue is null, something went wrong");
        if (cameraLogic != null)
        {
            Assert.AreEqual(cameraLogic, logicAndControllerPropertyPair.CameraLogic, "CameraLogicHandler.PopCameraLogic top of queue isnt expected camera logic");
        }
        logicAndControllerPropertyPair.CameraLogic?.OnPop(this);
        CurrentProperties = logicAndControllerPropertyPair.PreviousLogicProperties;
    }

    public Vector2 ClampWorldPositionInsideCamera2D(Vector2 position)
    {
        Camera camera = attachedCamera;
        Vector2 camPos = camera.transform.position;
        float aspect = camera.aspect;
        Assert.IsTrue(camera.orthographic, "CameraManager ClampWorldPositionInsideScreen2D expects camera to be orthographic");
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.orthographicSize * aspect;

        Vector2 localPos = position - camPos;
        localPos.y = Mathf.Clamp(localPos.y, -halfHeight, halfHeight);
        localPos.x = Mathf.Clamp(localPos.x, -halfWidth, halfWidth);

        return camPos + localPos;
    }

    void Awake()
    {
        CameraLogicStack = new Stack<CameraLogicAndControllerPropertyPair>();
        attachedCamera = GetComponent<Camera>();
        Assert.IsNotNull(attachedCamera, "CameraController.Awake CameraController expects an attached camera");

        Assert.IsNull(Common.Game.Instance.World.Camera, "CameraController.Awake More than one registered Camera");
        Common.Game.Instance.World.Camera = this;
    }

    void Start()
    {
        CurrentProperties = DefaultProperties;
        PushCameraLogic(BaseCameraLogic);
    }

    void OnApplicationQuit()
    {
        CameraLogicStack.Clear();
        Common.Game.Instance.World.Camera = null;
    }

    void LateUpdate()
    {
        CameraLogicStack.Peek()?.CameraLogic?.OnLateUpdate(this);
    }

    void FixedUpdate()
    {
        CurrentProperties.TargetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, CurrentProperties.TargetPosition, ref velocity, CurrentProperties.SmoothTime);
        attachedCamera.orthographicSize = Mathf.SmoothDamp(attachedCamera.orthographicSize, CurrentProperties.TargetOrthographicSize, ref zoomSpeed, CurrentProperties.ZoomSmoothTime);
    }

}
