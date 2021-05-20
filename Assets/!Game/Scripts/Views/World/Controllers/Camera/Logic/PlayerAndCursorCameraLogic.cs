using Project.Views.World;
using UnityEngine;

namespace Project.Views.Controllers
{
    [CreateAssetMenu(fileName = "PlayerAndCursorCameraLogic", menuName = "ScriptableObjects/CameraLogic/PlayerAndCursorCameraLogic", order = 1)]
    public class PlayerAndCursorCameraLogic : CameraLogic
    {
        /// <summary>
        /// The extent that the cursor's position will influence the camera's position. \n
        /// More specifically, cameraPos = (1-x) * playerPos + (x) * cursorPos
        /// </summary>
        [SerializeField]
        [Range(0.0f, 0.5f)]
        private float cursorBias = 0.25f;

        public override void UpdateCamera(CameraController camera, WorldView view)
        {
            Vector3 clampedCursorPosition = camera.ClampWorldPositionInsideCamera2D(view.CursorPosition);
            Vector3 position = Vector3.Lerp(view.PlayerPosition, clampedCursorPosition, cursorBias);
            camera.Settings.targetPosition = position;
        }
    }
}
