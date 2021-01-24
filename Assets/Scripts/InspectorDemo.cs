using UnityEngine;

namespace DefaultNamespace
{
    public class InspectorDemo : BaseDemo
    {
        [SerializeField]
        private string[] names;
        
        [SerializeField]
        private string message;

        public override string GetName(int i)
        {
            return names[i];
        }

        public override string GetMessage()
        {
            return message;
        }
    }
}
