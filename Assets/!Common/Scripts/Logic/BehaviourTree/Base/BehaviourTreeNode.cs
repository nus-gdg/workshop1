using XNode;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace Common.Logic
{
    public abstract class BehaviourTreeNode : Node
    {
        public enum Status
        {
            Ready, Completed, Running, Failed, 
        }

        public BehaviourTree Graph => graph as BehaviourTree;
        public abstract Status Evaluate(BehaviourTreeController controller);

        public override object GetValue(NodePort nodePort)
        {
            return this;
        }
    }

    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(BehaviourTreeNode))]
    public class BehaviourTreeNodeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, GUIContent.none);
        }
    }
    #endif
}
