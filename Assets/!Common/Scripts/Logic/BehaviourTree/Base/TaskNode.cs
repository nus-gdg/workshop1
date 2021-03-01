using UnityEngine;
#if UNITY_EDITOR
using XNodeEditor;
#endif

namespace Common.Logic
{
    public abstract class TaskNode : BehaviourTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [SerializeField]
        protected BehaviourTreeNode parent;
    }
    
    #if UNITY_EDITOR
    [CustomNodeEditor(typeof(TaskNode))]
    public class TaskNodeEditor : BehaviourTreeNodeEditor
    {
        /// <summary> Draws standard field editors for all public fields </summary>
        public override void OnBodyGUI()
        {
            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("parent"));
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
