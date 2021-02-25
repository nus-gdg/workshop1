using UnityEngine;
using XNode;
#if UNITY_EDITOR
using XNodeEditor;
#endif

namespace Common.Logic
{
    public abstract class DecoratorNode : BehaviourTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [HideInInspector]
        [SerializeField]
        protected BehaviourTreeNode parent;

        [Output(connectionType = ConnectionType.Override)]
        [HideInInspector]
        [SerializeField]
        protected BehaviourTreeNode child;

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
            if (!outputPort.IsConnected)
            {
                child = null;
                return;
            }
            var node = outputPort.GetConnection(0).node as BehaviourTreeNode;
            if (node == null)
            {
                child = null;
                return;
            }
            child = node;
        }
        
        #if UNITY_EDITOR
        [CustomNodeEditor(typeof(DecoratorNode))]
        public class DecoratorNodeEditor : NodeEditor
        {
            public override void OnBodyGUI()
            {
                NodeEditorGUILayout.PortPair(target.GetPort("parent"), target.GetPort("child"));
                base.OnBodyGUI();
            }
        }
        #endif
    }
}
