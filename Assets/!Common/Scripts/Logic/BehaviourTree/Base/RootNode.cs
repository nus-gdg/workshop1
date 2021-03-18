using UnityEngine;
#if UNITY_EDITOR
using XNodeEditor;
#endif

namespace Common.Logic
{
    [DisallowMultipleNodes]
    public class RootNode : BehaviourTreeNode
    {
        [Output(connectionType = ConnectionType.Override)]
        [SerializeField]
        private BehaviourTreeNode child;

        public override void ResetController(BehaviourTreeController controller)
        {
            base.ResetController(controller);
            if (child == null)
            {
                return;
            }
            child.ResetController(controller);
        }
        
        public override void RemoveController(BehaviourTreeController controller)
        {
            base.RemoveController(controller);
            if (child == null)
            {
                return;
            }
            child.RemoveController(controller);
        }

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (child == null)
            {
                return Status.Completed;
            }
            if (!child.IsStatus(controller, Status.Running))
            {
                child.SetStatus(controller, Status.Ready);
            }
            return child.Tick(controller);
        }

        protected override void Serialize()
        {
            // Serialize child port
            var outputPort = GetOutputPort("child");
            child = GetConnectedNode(outputPort);
        }
    }

    #if UNITY_EDITOR
    [CustomNodeEditor(typeof(RootNode))]
    public class RootNodeEditor : BehaviourTreeNodeEditor
    {
        /// <summary> Draws standard field editors for all public fields </summary>
        public override void OnBodyGUI()
        {
            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("child"));
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
