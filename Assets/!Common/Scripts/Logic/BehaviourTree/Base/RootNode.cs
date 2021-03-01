using UnityEngine;
using XNode;
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

        public override void Load(BehaviourTreeController controller)
        {
            base.Load(controller);
            if (child == null)
            {
                return;
            }
            child.Load(controller);
        }
        
        public override void Unload(BehaviourTreeController controller)
        {
            base.Unload(controller);
            if (child == null)
            {
                return;
            }
            child.Unload(controller);
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

        protected override void Init()
        {
            OnValidate();
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            OnValidate();
        }

        public override void OnRemoveConnection(NodePort port)
        {
            OnValidate();
        }

        private void OnValidate()
        {
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
