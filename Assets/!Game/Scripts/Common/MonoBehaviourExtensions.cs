using UnityEngine;

namespace Project.Common
{
    public static class MonoBehaviourExtensions
    {
        public static T GetComponentIfMissing<T>(this MonoBehaviour behaviour, T component)
            where T : Component
        {
            if (component != null)
            {
                return component;
            }
            var foundComponent = behaviour.GetComponent<T>();
            if (foundComponent == null)
            {
                throw new MissingComponentException($"Missing component: {typeof(T)}");
            }
            return foundComponent;
        }
    }
}
