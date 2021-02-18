using UnityEngine;
using World.Camera;
using UnityEngine.SceneManagement;

namespace Core.Managers
{
    public class WorldManager : MonoBehaviour
    {
        public Entity.Player Player { get; private set; }
        public Ui.Cursor Cursor { get; set; }

        public CameraController Camera { get; set; }
        private Scene persistentObjectsScene;
        // TODO: Add system to pause entities

        public void RegisterPlayer(Entity.Player player)
        {
            if (Player != null)
            {
                player.gameObject.SetActive(false);
                Player.gameObject.transform.position = player.gameObject.transform.position;
            }
            else
            {
                Player = player;
                Player.transform.parent = null;
                SceneManager.MoveGameObjectToScene(Player.gameObject, GetPersistentObjectScene());
            }
        }

        void OnApplicationQuit()
        {
            Player = null;
            Cursor = null;
        }

        Scene GetPersistentObjectScene()
        {
            if (!persistentObjectsScene.IsValid())
                persistentObjectsScene = SceneManager.CreateScene("Persistent Objects");
            return persistentObjectsScene;
        }
    }
}
