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
public class CameraLogicHandler : MonoBehaviour
{
    public ICameraLogic BaseCameraLogic;
    private CameraController cameraController;
    private Camera attachedCamera;

    private class CameraLogicAndControllerPropertyPair
    {
        public ICameraLogic CameraLogic;
        public CameraController.ControllerProperties PreviousLogicProperties;
    }

    private Stack<CameraLogicAndControllerPropertyPair> CameraLogicStack;

    void Awake()
    {
        CameraLogicStack = new Stack<CameraLogicAndControllerPropertyPair>();
        attachedCamera = GetComponent<Camera>();
        Assert.IsNotNull(attachedCamera, "CameraLogicHandler.Awake CameraController expects an attached camera");
        cameraController = GetComponent<CameraController>();
        Assert.IsNotNull(cameraController, "CameraLogicHandler.Awake CameraController expects a CameraController");
    }

    void Start()
    {
        PushCameraLogic(BaseCameraLogic);
    }

    void OnApplicationQuit()
    {
        CameraLogicStack.Clear();
    }

    /// <summary>
    /// Pushes a new camera logic
    /// </summary>
    /// <param name="cameraLogic">Camera Logic to push</param>
    public void PushCameraLogic(ICameraLogic cameraLogic)
    {
        CameraLogicAndControllerPropertyPair logicAndControllerPropertyPair = new CameraLogicAndControllerPropertyPair();
        logicAndControllerPropertyPair.CameraLogic = cameraLogic;
        logicAndControllerPropertyPair.PreviousLogicProperties = cameraController.CurrentProperties; // copy
        CameraLogicStack.Push(logicAndControllerPropertyPair);
        cameraLogic.OnPush(cameraController);
    }

    /// <summary>
    /// Pops camera logic off the stack. If you provide the camera logic, will do runtime check if cameralogic was indeed on top of stack
    /// </summary>
    /// <param name="cameraLogic">Camera Logic to check</param>
    public void PopCameraLogic(ICameraLogic cameraLogic = null)
    {
        CameraLogicAndControllerPropertyPair logicAndControllerPropertyPair = CameraLogicStack.Pop();
        Assert.IsNotNull(logicAndControllerPropertyPair, "CameraLogicHandler.PopCameraLogic top of queue is null, something went wrong");
        if (cameraLogic != null)
        {
            Assert.AreEqual(cameraLogic, CameraLogicStack.Peek().CameraLogic, "CameraLogicHandler.PopCameraLogic top of queue isnt expected camera logic");
        }
        logicAndControllerPropertyPair.CameraLogic?.OnPop(cameraController);
        cameraController.CurrentProperties = logicAndControllerPropertyPair.PreviousLogicProperties;
    }

    void LateUpdate()
    {
        CameraLogicStack.Peek()?.CameraLogic?.OnLateUpdate(cameraController);
    }
}

