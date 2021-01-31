using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Common
{
    public class CameraManager : MonoBehaviour
    {
        public void OnSceneLoadedByWorld(Scene scene)
        {
            // disable all new camera at root
            // we can do more here
            // if this is too slow we can do setup per scene for optimization but this is more convenient for everyone at the moment
            foreach (GameObject go in scene.GetRootGameObjects())
            {
                if (go.GetComponent<Camera>())
                {
                    go.SetActive(false);
                }
            }
        }

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
