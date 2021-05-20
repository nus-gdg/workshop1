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

        public override void Init()
        {
            camera.Init();
            player.Init();
            cursor.Init();
        }

        public Vector3 GetWorldMousePosition()
        {
            return camera.GetWorldMousePosition();
        }
    }
}
