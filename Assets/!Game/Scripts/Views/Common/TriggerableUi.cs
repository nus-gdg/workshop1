using UnityEngine;
using UnityEngine.Events;

namespace Project.Views.Common
{
    public class TriggerableUi : MonoBehaviour
    {
        [SerializeField]
        private bool debug;

        [SerializeField]
        private UnityEvent onTrigger;

        private void Start()
        {
            if (GetComponent<Collider2D>() == null)
            {
                Debug.LogError($"{name}: {nameof(TriggerableUi)} requires a Collider");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (debug)
            {
                Debug.Log($"Triggered: {name}");
            }
            onTrigger.Invoke();
        }
    }
}
