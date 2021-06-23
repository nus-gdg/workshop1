using System;
using System.Collections.Generic;
using Project.Models.Levels;
using Project.Views.Controllers;
using Project.Views.World.Entities;
using UnityEngine;

namespace Project.Views.World
{
    public class WorldView : View
    {
        [SerializeField]
        private new CameraController camera;

        [SerializeField]
        private PlayerUi player;

        [SerializeField]
        private PlayerCursorUi cursor;

        [SerializeField]
        private List<EnemyUi2> enemies;

        public Vector3 PlayerPosition => player.transform.position;
        public Vector3 CursorPosition => cursor.transform.position;

        public override void Init()
        {
            camera.Init(this);
            player.Init(this);
            cursor.Init(this);

            foreach (var enemy in enemies)
            {
                enemy.Init(this);
            }
        }

        public Vector3 GetWorldMousePosition()
        {
            return camera.GetWorldMousePosition();
        }

        public void PushCameraLogic(CameraLogic cameraLogic)
        {
            camera.PushCameraLogic(cameraLogic);
        }

        public void PopCameraLogic()
        {
            camera.PopCameraLogic();
        }

        public bool RequestLoadLevel(Level level)
        {
            throw new NotImplementedException();
        }
    }
}
