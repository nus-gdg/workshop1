using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;
#if UNITY_EDITOR
using XNodeEditor;
#endif

namespace Common.Logic
{
    public abstract class CompositeNode : BehaviourTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [SerializeField]
        protected BehaviourTreeNode parent;

        [Output(connectionType = ConnectionType.Override, dynamicPortList = true)]
        [SerializeField]
        protected List<BehaviourTreeNode> children;

        public override void ResetController(BehaviourTreeController controller)
        {
            base.ResetController(controller);
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                if (child == null)
                {
                    continue;
                }

                child.ResetController(controller);
            }
        }

        public override void RemoveController(BehaviourTreeController controller)
        {
            base.RemoveController(controller);
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                if (child == null)
                {
                    continue;
                }

                child.RemoveController(controller);
            }
        }

        public override void Enter(BehaviourTreeController controller)
        {
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                if (child == null)
                {
                    continue;
                }
                child.SetStatus(controller, Status.Ready);
            }
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
                children[i] = GetConnectedNode(outputPort);
            }
        }
    }

    #if UNITY_EDITOR
    [CustomNodeEditor(typeof(CompositeNode))]
    public class CompositeNodeEditor : BehaviourTreeNodeEditor
    {
        /// <summary> Draws standard field editors for all public fields </summary>
        public override void OnBodyGUI()
        {
            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("parent"));
            DrawProperties();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("children"));
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}