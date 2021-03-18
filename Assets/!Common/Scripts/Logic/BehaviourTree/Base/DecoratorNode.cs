using UnityEngine;
#if UNITY_EDITOR
using XNodeEditor;
#endif

namespace Common.Logic
{
    public abstract class DecoratorNode : BehaviourTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [SerializeField]
        protected BehaviourTreeNode parent;

        [Output(connectionType = ConnectionType.Override)]
        [SerializeField]
        protected BehaviourTreeNode child;

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
        
        public override void Enter(BehaviourTreeController controller)
        {
            if (child == null)
            {
                return;
            }
            child.SetStatus(controller, Status.Ready);
        }

        protected override void Serialize()
        {
            // Serialize child port
            var outputPort = GetOutputPort("child");
            child = GetConnectedNode(outputPort);
        }

        #if UNITY_EDITOR
        [CustomNodeEditor(typeof(DecoratorNode))]
        public class DecoratorNodeEditor : BehaviourTreeNodeEditor
        {
            /// <summary> Draws standard field editors for all public fields </summary>
            public override void OnBodyGUI()
            {
                serializedObject.Update();
                var node = (DecoratorNode)serializedObject.targetObject;
                NodeEditorGUILayout.PortPair(node.GetPort("parent"), node.GetPort("child"));
                DrawProperties();
                serializedObject.ApplyModifiedProperties();
            }
        }
        #endif
    }
}
