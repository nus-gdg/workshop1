using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct Function
{
    public FunctionOperation[] Operations;
    public float Evaluate(float value)
    {
        foreach (FunctionOperation operation in Operations)
        {
            value = operation.Evaluate(value);
        }
        return value;
    }
}

[Serializable]
public struct FunctionOperation
{
    public enum OperationType
    {
        Add,
        Multiply
    }
    public OperationType Operation;
    public float Value;

    public float Evaluate(float initialValue)
    {
        switch (Operation)
        {
            case OperationType.Add:
                return initialValue + Value;
            case OperationType.Multiply:
                return initialValue * Value;
            default:
                return initialValue;
        }
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(FunctionOperation))]
public class FunctionOperationEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        Rect operationRect = new Rect(position.x, position.y, 100, position.height);
        Rect valueRect = new Rect(position.x + 115, position.y, 100, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("Value"), GUIContent.none);
        EditorGUI.PropertyField(operationRect, property.FindPropertyRelative("Operation"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
#endif