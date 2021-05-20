using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.Views.Common
{
    public class HoverableUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private bool debug;

        [SerializeField]
        private UnityEvent onEnter;

        [SerializeField]
        private UnityEvent onExit;

        private void Start()
        {
            if (GetComponent<Collider2D>() == null)
            {
                Debug.LogError($"{name}: {nameof(ClickableUi)} requires a Collider");
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (debug)
            {
                Debug.Log($"Hover (Enter): {name}");
            }
            onEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (debug)
            {
                Debug.Log($"Hover (Exit): {name}");
            }
            onExit.Invoke();
        }
    }
}
