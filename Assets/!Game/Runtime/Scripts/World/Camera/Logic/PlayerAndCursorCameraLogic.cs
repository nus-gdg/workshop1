﻿using UnityEngine;
using UnityEngine.Assertions;

namespace World.Camera
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
        private float CursorBias = 0.25f;
        public override void OnLateUpdate(CameraController controller)
        {
            Entity.Player player = Core.Game.Instance.World.Player;
            Ui.Cursor cursor = Core.Game.Instance.World.Cursor;

            if (player == null || cursor == null)
            {
                return;
            }

            Vector3 cursorPosition = controller.ClampWorldPositionInsideCamera2D(cursor.transform.position);
            Vector3 targetPosition = Vector3.Lerp(player.transform.position, cursorPosition, CursorBias);
            controller.CurrentSettings.TargetPosition = targetPosition;
        }
    }
}