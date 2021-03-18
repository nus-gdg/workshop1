using System.Collections.Generic;
using UnityEngine;
using XNode;
#if UNITY_EDITOR
using XNodeEditor;
#endif

namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Composite/Monitor", -100)]
    public class Monitor : BehaviourTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [SerializeField]
        protected BehaviourTreeNode parent;

        [Output(connectionType = ConnectionType.Override)]
        [SerializeField]
        protected BehaviourTreeNode condition;
        
        [Output(connectionType = ConnectionType.Override)]
        [SerializeField]
        protected BehaviourTreeNode child;
        
        public float frequency;

        private Dictionary<BehaviourTreeController, float> _timersOfControllers =
            new Dictionary<BehaviourTreeController, float>();

        public override void ResetController(BehaviourTreeController controller)
        {
            base.ResetController(controller);
            if (condition != null)
            {
                condition.ResetController(controller);
            }
            if (child != null)
            {
                child.ResetController(controller);
            }
        }

        public override void RemoveController(BehaviourTreeController controller)
        {
            base.RemoveController(controller);
            if (condition != null)
            {
                condition.RemoveController(controller);
            }
            if (child != null)
            {
                child.RemoveController(controller);
            }
        }

        public override void Enter(BehaviourTreeController controller)
        {
            if (condition != null)
            {
                condition.SetStatus(controller, Status.Ready);
                _timersOfControllers[controller] = Time.time + frequency;
            }
            if (child != null)
            {
                child.SetStatus(controller, Status.Ready);
            }
        }

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (child == null)
            {
                return Status.Completed;
            }
            var result = child.Tick(controller);
            if (result == Status.Failed)
            {
                Graph.AddMonitor(controller, this);
            }

            return result;
        }

        public Status TickCondition(BehaviourTreeController controller)
        {
            if (condition == null)
            {
                return Status.Failed;
            }
            if (_timersOfControllers[controller] > Time.time)
            {
                return Status.Running;
            }
            _timersOfControllers[controller] += frequency;

            var result = condition.Tick(controller);
            SetStatus(controller, result);

            return result;
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
            condition = GetConnectedNode(GetOutputPort("condition"));
            child = GetConnectedNode(GetOutputPort("child"));
        }
    }

    #if UNITY_EDITOR
    [CustomNodeEditor(typeof(Monitor))]
    public class MonitorNodeEditor : BehaviourTreeNodeEditor
    {
        /// <summary> Draws standard field editors for all public fields </summary>
        public override void OnBodyGUI()
        {
            serializedObject.Update();
            var node = (Monitor)serializedObject.targetObject;
            NodeEditorGUILayout.PortPair(node.GetPort("parent"), node.GetPort("condition"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("child"));
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
