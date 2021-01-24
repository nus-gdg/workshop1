using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DemoController : MonoBehaviour
    {
        [SerializeField]
        private Text nameUi;
        [SerializeField]
        private Text textUi;

        [SerializeField]
        private BaseDemo demo;

        [SerializeField, Range(0, 9)]
        private int nameIndex;

        public void Update()
        {
            nameUi.text = demo.GetName(nameIndex);
            textUi.text = demo.GetMessage();
        }
    }
}
