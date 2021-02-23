using UnityEngine;
using XNode;

namespace Common.Logic
{
    public abstract class DecoratorNode : BehaviorTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [SerializeField]
        protected BehaviorTreeNode parent;

        [Output(connectionType = ConnectionType.Override)]
        [SerializeField]
        protected BehaviorTreeNode child;

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
            var node = outputPort.GetConnection(0).node as BehaviorTreeNode;
            if (node == null)
            {
                child = null;
                return;
            }
            child = node;
        }
    }
}
