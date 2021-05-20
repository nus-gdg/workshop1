using System.Collections.Generic;
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
        private List<EnemyUi> enemies;

        public Vector3 PlayerPosition => player.transform.position;
        public Vector3 CursorPosition => cursor.transform.position;

        public override void Init()
        {
            camera.Init();
            player.Init();
            cursor.Init();

            foreach (var enemy in enemies)
            {
                enemy.Init();
            }
        }

        public Vector3 GetWorldMousePosition()
        {
            return camera.GetWorldMousePosition();
        }
    }
}
