using XNode;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace Common.Logic
{
    public abstract class BehaviorTreeNode : Node
    {
        public enum Status
        {
            Ready, Completed, Running, Failed, 
        }

        public BehaviorTree Graph => graph as BehaviorTree;
        public abstract Status Evaluate(BehaviorTreeController controller);

        public override object GetValue(NodePort nodePort)
        {
            return this;
        }
    }

    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(BehaviorTreeNode))]
    public class BehaviorTreeNodeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, GUIContent.none);
        }
    }
    #endif
}
