﻿using UnityEngine;

namespace World.Camera
{
    [System.Serializable]
    public struct CameraControllerSettings
    {
        public Vector3 TargetPosition;
        public float SmoothTime;
        public float TargetOrthographicSize;
        public float ZoomSmoothTime;
    }
}