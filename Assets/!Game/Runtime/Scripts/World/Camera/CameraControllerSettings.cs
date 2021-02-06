using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CameraControllerSettings
{
    public Vector3 TargetPosition;
    public float SmoothTime;
    public float TargetOrthographicSize;
    public float ZoomSmoothTime;
}
