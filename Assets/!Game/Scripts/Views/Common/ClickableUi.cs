using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.Views.Common
{
    public class ClickableUi : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private bool debug;

        [SerializeField]
        public UnityEvent onClick;

        private void Start()
        {
            if (GetComponent<Collider2D>() == null)
            {
                Debug.LogError($"{name}: {nameof(ClickableUi)} requires a Collider");
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (debug)
            {
                Debug.Log($"Clicked: {name}");
            }
            onClick.Invoke();
        }
    }
}
