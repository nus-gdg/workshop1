using System;
using System.Collections.Generic;
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
            Completed, Running, Failed, Ready, Invalid, 
        }

        public BehaviourTree Graph => graph as BehaviourTree;

        private Dictionary<BehaviourTreeController, Status> _statusOfControllers =
            new Dictionary<BehaviourTreeController, Status>();

        public virtual void Load(BehaviourTreeController controller)
        {
            _statusOfControllers[controller] = Status.Ready;
        }
        
        public virtual void Unload(BehaviourTreeController controller)
        {
            _statusOfControllers.Remove(controller);
        }

        public Status GetStatus(BehaviourTreeController controller)
        {
            if (!_statusOfControllers.TryGetValue(controller, out Status status))
            {
                return Status.Ready;
            }
            return status;
        }
        
        public bool IsStatus(BehaviourTreeController controller, Status status)
        {
            if (!_statusOfControllers.TryGetValue(controller, out Status internalStatus))
            {
                return false;
            }
            return status == internalStatus;
        }
        
        public void SetStatus(BehaviourTreeController controller, Status status)
        {
            _statusOfControllers[controller] = status;
        }

        public Status Tick(BehaviourTreeController controller)
        {
            var result = Status.Completed;

            if (!IsStatus(controller, Status.Running))
            {
                Enter(controller);
            }

            result = Evaluate(controller);

            if (result != Status.Running)
            {
                Exit(controller);
            }

            SetStatus(controller, result);
            return result;
        }

        public virtual Status Evaluate(BehaviourTreeController controller)
        {
            throw new NotImplementedException($"{name} is missing an evaluate function.");
        }

        public virtual void Enter(BehaviourTreeController controller) { }
        public virtual void Exit(BehaviourTreeController controller) { }

        protected BehaviourTreeNode GetConnectedNode(NodePort port)
        {
            if (!port.IsConnected)
            {
                return null;
            }
            return port.GetConnection(0).node as BehaviourTreeNode;
        }

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
