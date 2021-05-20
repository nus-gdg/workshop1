using Project.Views.World;
using UnityEngine;

namespace Project.Views.Controllers
{
    public abstract class CameraLogic : ScriptableObject
    {
        public virtual void InitCamera(CameraController camera, WorldView view) { }
        public virtual void UpdateCamera(CameraController camera, WorldView view) { }
    }
}
