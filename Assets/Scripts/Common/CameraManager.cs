using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common
{
    public class CameraManager : MonoBehaviour
    {
        public CameraLogicHandler CameraLogicHandler;

        public static Vector2 ClampWorldPositionInsideCamera2D(Vector2 position, Camera camera)
        {
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
    }

}
