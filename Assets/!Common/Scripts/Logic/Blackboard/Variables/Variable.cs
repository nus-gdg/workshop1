using System;
using UnityEngine;

namespace Common.Logic
{
    public abstract class Variable<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        private T value;
        
        [NonSerialized]
        public T Value;

        public void OnAfterDeserialize()
        {
            Value = value;
        }

        public void OnBeforeSerialize() { }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
