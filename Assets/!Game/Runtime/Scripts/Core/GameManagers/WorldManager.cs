using UnityEngine;

namespace Common
{
    public class WorldManager : MonoBehaviour
    {
        public Entity.Player Player { get; set; }
        public Entity.Cursor Cursor { get; set; }

        public CameraController Camera { get; set; }

        // TODO: Add system to pause entities

        void OnApplicationQuit()
        {
            Player = null;
            Cursor = null;
        }
    }
}
