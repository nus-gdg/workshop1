using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Demo : MonoBehaviour
    {
        [SerializeField]
        private Text nameUi;
        [SerializeField]
        private Text textUi;

        [SerializeField]
        private string[] names;
        [SerializeField]
        private int nameIndex;

        public void Update()
        {
            nameUi.text = $"{names[nameIndex]}\n";
            textUi.text = $"Thank you for coming!\n";
        }
    }
}
