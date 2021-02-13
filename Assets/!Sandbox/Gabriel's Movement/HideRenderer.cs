using UnityEngine;

namespace DefaultNamespace
{
    public class HideRenderer : MonoBehaviour
    {
        private Renderer _renderer;

        public void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.enabled = false;
        }
    }
}
