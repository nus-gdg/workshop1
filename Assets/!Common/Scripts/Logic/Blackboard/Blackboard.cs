using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Common.Logic
{
    [Serializable]
    public class Blackboard : Dictionary<BlackboardKey, object>
    {
        [Serializable]
        private class BlackboardVariable
        {
            public BlackboardKey key;
            public UnityEngine.Object value;
        }

        [SerializeField]
        private BlackboardVariable[] variables;

        public void Refresh()
        {
            Clear();
            for (int i = 0; i < variables.Length; i++)
            {
                var variable = variables[i];
                this[variable.key] = variable.value;
            }
        }

        public bool TryGetValue<T>(BlackboardKey key, out T value)
        {
            if (base.TryGetValue(key, out object obj))
            {
                if (obj is T)
                {
                    value = (T) obj;
                    return true;
                }
            }
            value = default(T);
            return false;
        }
    }
    
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Blackboard))]
    public class BlackboardEditor : PropertyDrawer
    {
        private ReorderableList _list;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var variables = property.FindPropertyRelative("variables");
            var list = GetList(variables);

            float height = 0f;
            for(var i = 0; i < variables.arraySize; i++)
            {
                height = Mathf.Max(height, EditorGUI.GetPropertyHeight(variables.GetArrayElementAtIndex(i)));
            }
            list.elementHeight = height;
            list.DoList(position);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return GetList(property.FindPropertyRelative("variables")).GetHeight();
        }

        public ReorderableList GetList(SerializedProperty property)
        {
            if (_list == null)
            {
                _list = new ReorderableList(property.serializedObject, property, true, true, true, true);
                _list.drawHeaderCallback += DrawHeader;
                _list.drawElementCallback += DrawElements;
            }

            return _list;
        }

        public void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Variables");
        }
        
        public void DrawElements(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (_list == null)
            {
                return;
            }

            SerializedProperty element = _list.serializedProperty.GetArrayElementAtIndex(index);
            
            Rect first = new Rect(
                rect.x,
                rect.y,
                rect.width * 0.45f,
                rect.height);

            Rect second = new Rect(
                rect.x + rect.width / 2,
                rect.y,
                rect.width * 0.45f,
                rect.height);
            
            EditorGUI.PropertyField(first, element.FindPropertyRelative("key"), GUIContent.none);
            EditorGUI.PropertyField(second, element.FindPropertyRelative("value"), GUIContent.none);
        }
    }
    #endif
}
