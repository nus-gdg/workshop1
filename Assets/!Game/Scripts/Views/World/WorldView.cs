using Project.Views.World.Entities;
using UnityEngine;

namespace Project.Views.World
{
    public class WorldView : View
    {
        [SerializeField]
        private PlayerUi player;

        public override void Init()
        {
            player.Init();
        }
    }
}
