using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICameraLogic : ScriptableObject
{
    public virtual void OnPush(CameraController controller) { }
    public virtual void OnPop(CameraController controller) { }
    public virtual void OnLateUpdate(CameraController controller) { }
}

