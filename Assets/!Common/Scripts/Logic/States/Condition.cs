using UnityEngine;

namespace Common.Logic.States
{
    /**
     * Base class for any plug and play condition
     */
    public abstract class Condition<T> : ScriptableObject
    {
        // Returns true if the controller meets the condition
        public abstract bool IsTrue(T controller);
    }
}
