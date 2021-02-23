using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Common.Logic
{
    public abstract class CompositeNode : BehaviorTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [SerializeField]
        protected BehaviorTreeNode parent;

        [Output(connectionType = ConnectionType.Override, dynamicPortList = true)]
        [SerializeField]
        protected List<BehaviorTreeNode> children;

        [ContextMenu("Set Root")]
        public void SetRoot()
        {
            Graph.root = this;
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
            var outputPorts = DynamicOutputs.ToArray();
            for (int i = 0; i < outputPorts.Length; i++)
            {
                var outputPort = outputPorts[i];
                if (!outputPort.IsConnected)
                {
                    children[i] = null;
                    continue;
                }
                var node = outputPort.GetConnection(0).node as BehaviorTreeNode;
                if (node == null)
                {
                    children[i] = null;
                    continue;
                }

                children[i] = node;
            }
        }
    }
}
