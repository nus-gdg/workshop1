using UnityEngine;
using UnityEngine.Events;

namespace Project.Views.Common
{
    public class InteractableUi : MonoBehaviour
    {
        [SerializeField]
        private bool debug;

        [SerializeField]
        private UnityEvent onInteract;

        private void Start()
        {
            if (GetComponent<Collider2D>() == null)
            {
                Debug.LogError($"{name}: {nameof(InteractableUi)} requires a Collider");
            }
        }

        public void Interact()
        {
            if (debug)
            {
                Debug.Log($"Interacted: {name}");
            }
            onInteract.Invoke();
        }
    }
}
