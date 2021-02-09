using UnityEngine;

public abstract class CameraLogic : ScriptableObject
{
    public virtual void OnPush(CameraController controller) { }
    public virtual void OnPop(CameraController controller) { }
    public virtual void OnLateUpdate(CameraController controller) { }
}

