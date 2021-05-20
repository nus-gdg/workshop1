using UnityEngine;

namespace Project.Views.Common
{
    public class HighlightableUi : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Color normalColour = Color.grey;

        [SerializeField]
        private Color highlightedColour = Color.white;

        [SerializeField]
        private bool debug;

        private void OnEnable()
        {
            spriteRenderer.color = normalColour;
        }

        public void ShowHighlight()
        {
            if (debug)
            {
                Debug.Log($"Show Highlight: {name}");
            }
            spriteRenderer.color = highlightedColour;
        }

        public void HideHighlight()
        {
            if (debug)
            {
                Debug.Log($"Hide Highlight: {name}");
            }
            spriteRenderer.color = normalColour;
        }

        private void OnValidate()
        {
            spriteRenderer.color = normalColour;
        }
    }
}
