using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Core.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Experimental
{
    [Serializable]
    public abstract class GameNode
    {
        [SerializeField] protected string NodeName = "Node Name";
        public abstract void Evaluate(GameContext context);
    }

    // example!
    public class DebugLogNode : GameNode
    {
        public string DebugText;

        public DebugLogNode()
        {
            NodeName = "Debug Log Node";
        }

        public override void Evaluate(GameContext context)
        {
            Debug.Log(DebugText);
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    public class SubclassSelectorAttribute : PropertyAttribute
    {
        Type m_type;
        public SubclassSelectorAttribute(System.Type type)
        {
            m_type = type;
        }

        public Type GetFieldType()
        {
            return m_type;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        bool initializeFold = false;
        List<System.Type> reflectionType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference) return;

            EditorGUI.DrawRect(position, new Color(0.1f, 0.1f, 0.1f, 0.2f));
            position.y += 10;
            position.height += 10;

            SubclassSelectorAttribute utility = (SubclassSelectorAttribute)attribute;
            LazyGetAllInheritedType(utility.GetFieldType());
            Rect popupPosition = GetPopupPosition(position);

            string[] typePopupNameArray = reflectionType.Select(type => type == null ? "<null>" : type.Name).ToArray();
            string[] typeFullNameArray = reflectionType.Select(type => type == null ? "" : string.Format("{0} {1}", type.Assembly.ToString().Split(',')[0], type.FullName)).ToArray();

            //Get the type of serialized object 
            int currentTypeIndex = Array.IndexOf(typeFullNameArray, property.managedReferenceFullTypename);
            Type currentObjectType = reflectionType[currentTypeIndex];

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            int selectedTypeIndex = EditorGUI.Popup(popupPosition, "", currentTypeIndex, typePopupNameArray);
            if (selectedTypeIndex >= 0 && selectedTypeIndex < reflectionType.Count)
            {
                if (currentObjectType != reflectionType[selectedTypeIndex])
                {
                    if (reflectionType[selectedTypeIndex] == null)
                        property.managedReferenceValue = null;
                    else
                        property.managedReferenceValue = Activator.CreateInstance(reflectionType[selectedTypeIndex]);

                    currentObjectType = reflectionType[selectedTypeIndex];
                }
            }
            EditorGUI.indentLevel = indent;

            EditorGUI.PropertyField(position, property, true);
            position.y += 10;

        }

        public void DrawUILine(Rect position, Color color, int thickness = 2, int padding = 10)
        {

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true) + 20;
        }

        void LazyGetAllInheritedType(System.Type baseType)
        {
            if (reflectionType != null) return;

            reflectionType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .ToList();
            reflectionType.Insert(0, null);
        }



        Rect GetPopupPosition(Rect currentPosition)
        {
            Rect popupPosition = new Rect(currentPosition);
            popupPosition.width -= EditorGUIUtility.labelWidth;
            popupPosition.x += EditorGUIUtility.labelWidth;
            popupPosition.height = EditorGUIUtility.singleLineHeight;
            return popupPosition;

        }
    }
#endif
}

