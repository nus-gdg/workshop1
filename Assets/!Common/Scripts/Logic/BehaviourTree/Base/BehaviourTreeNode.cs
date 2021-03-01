using XNode;
#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using XNodeEditor;
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
    [CustomNodeEditor(typeof(BehaviourTreeNode))]
    public class BehaviourTreeNodeEditor : NodeEditor
    {
        /// <summary> Draws standard field editors for all public fields </summary>
        public override void OnBodyGUI()
        {
            serializedObject.Update();
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }
        
        /// <summary> Draws standard field editors for all public fields </summary>
        protected void DrawProperties()
        {
            // Exclude properties with these names
            string[] excludes = {"m_Script", "graph", "position", "ports", "parent", "child", "children"};

            // Iterate through serialized properties and draw them like the Inspector (But with ports)
            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (excludes.Contains(iterator.name))
                {
                    continue;
                }
                NodeEditorGUILayout.PropertyField(iterator, true);
            }
        }
    }
    #endif
}
