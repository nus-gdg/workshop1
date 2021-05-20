using UnityEngine;

namespace Project.Views.Controllers
{
    [System.Serializable]
    public class CameraSettings
    {
        public Vector3 targetPosition;
        public float smoothTime;

        public float targetOrthographicSize;
        public float zoomSmoothTime;
    }
}
