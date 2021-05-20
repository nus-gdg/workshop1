using UnityEngine;

namespace Project.Views.Common
{
    public class CursorUi : MonoBehaviour
    {
        private void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}
