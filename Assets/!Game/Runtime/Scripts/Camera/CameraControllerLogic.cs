using System.Collections.Generic;
using UnityEngine.Assertions;

public class CameraControllerLogic
{
    private class CameraControllerLogicNode
    {
        public CameraLogic CameraLogic;
        public CameraControllerSettings PreviousControllerSettings;
    }
    private Stack<CameraControllerLogicNode> cameraLogicStack;
    private CameraController controller;

    public CameraControllerLogic(CameraController cameraController)
    {
        controller = cameraController;
        cameraLogicStack = new Stack<CameraControllerLogicNode>();
    }

    public void PushCameraLogic(CameraLogic cameraLogic, CameraControllerSettings currentControllerSettings)
    {
        CameraControllerLogicNode logicNode = new CameraControllerLogicNode();
        logicNode.CameraLogic = cameraLogic;
        logicNode.PreviousControllerSettings = currentControllerSettings; // copy
        cameraLogicStack.Push(logicNode);
        cameraLogic.OnPush(controller);
    }

    public void PopCameraLogic(CameraLogic cameraLogic, out CameraControllerSettings controllerSettings)
    {
        CameraControllerLogicNode logicNode = cameraLogicStack.Pop();
        Assert.IsNotNull(logicNode, "CameraControllerLogic.PopCameraLogic top of queue is null, something went wrong");
        if (cameraLogic != null)
        {
            Assert.AreEqual(cameraLogic, logicNode.CameraLogic, "CameraControllerLogic.PopCameraLogic top of queue isnt expected camera logic");
        }
        logicNode.CameraLogic?.OnPop(controller);
        controllerSettings = logicNode.PreviousControllerSettings;
    }

    public void ResetLogic()
    {
        cameraLogicStack.Clear();
    }

    public void LateUpdate()
    {
        cameraLogicStack.Peek()?.CameraLogic?.OnLateUpdate(controller);
    }
}
