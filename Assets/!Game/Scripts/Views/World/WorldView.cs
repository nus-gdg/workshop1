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

        public override void Init()
        {
            camera.Init();
            player.Init();
        }
    }
}
