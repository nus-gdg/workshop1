using System;
using System.Collections.Generic;
using XNode;
#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
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

        /// <summary>
        /// Returns the behaviour tree containing this node.
        /// </summary>
        public BehaviourTree Graph => graph as BehaviourTree;

        /// <summary>
        /// Maps the node status for each running controller.
        /// </summary>
        private Dictionary<BehaviourTreeController, Status> _statusOfControllers =
            new Dictionary<BehaviourTreeController, Status>();

        /// <summary>
        /// Resets the node status for the given controller.
        /// </summary>
        public virtual void ResetController(BehaviourTreeController controller)
        {
            _statusOfControllers[controller] = Status.Ready;
        }
        
        /// <summary>
        /// Clears the node status for the given controller.
        /// </summary>
        public virtual void RemoveController(BehaviourTreeController controller)
        {
            _statusOfControllers.Remove(controller);
        }

        /// <summary>
        /// Returns the node status for the given controller.
        /// </summary>
        public Status GetStatus(BehaviourTreeController controller)
        {
            try
            {
                return _statusOfControllers[controller];
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException($"{name} has not been loaded into the Behaviour Tree '{graph.name}'", e);
            }
        }

        /// <summary>
        /// Returns true if the node status for the given controller equals the given <paramref name="status"/>.
        /// </summary>
        public bool IsStatus(BehaviourTreeController controller, Status status)
        {
            return GetStatus(controller) == status;
        }

        /// <summary>
        /// Sets the node status for the given controller.
        /// </summary>
        public void SetStatus(BehaviourTreeController controller, Status status)
        {
            _statusOfControllers[controller] = status;
        }

        /// <summary>
        /// Updates the node status for the given controller.
        /// <para/>
        /// The following shows the sequence of events during a tick:
        /// <br/>
        /// Enter (first tick) -> Evaluate (every tick) -> Exit (last tick)
        /// </summary>
        public Status Tick(BehaviourTreeController controller)
        {
            var result = Status.Completed;

            // During the first tick, setup node settings
            if (!IsStatus(controller, Status.Running))
            {
                Enter(controller);
            }

            // Every tick, get the updated status for the controller
            result = Evaluate(controller);

            // During the last tick, teardown node settings
            if (result != Status.Running)
            {
                Exit(controller);
            }

            // Save the update status for the controller
            SetStatus(controller, result);
            return result;
        }

        /// <summary>
        /// Runs an update for the controller and returns the on-going status.
        /// </summary>
        public abstract Status Evaluate(BehaviourTreeController controller);

        /// <summary>
        /// Runs a setup for the controller before evaluating it.
        /// </summary>
        public virtual void Enter(BehaviourTreeController controller) { }
        
        /// <summary>
        /// Runs a teardown for the controller after evaluating it.
        /// </summary>
        public virtual void Exit(BehaviourTreeController controller) { }

        /// <summary>
        /// Serializes the references to connected nodes in the editor.
        /// </summary>
        protected BehaviourTreeNode GetConnectedNode(NodePort port)
        {
            var connections = port.GetConnections();

            // Sometimes there is an issue accessing the connected node.
            // There should only be one connection, so this returns null if the current state is invalid.
            if (connections.Count != 1)
            {
                return null;
            }

            return connections[0].node as BehaviourTreeNode;
        }

        public override object GetValue(NodePort nodePort)
        {
            // Return a dummy value since this method is not being used.
            return this;
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
            Serialize();
        }

        /// <summary>
        /// Serializes custom node properties.
        /// </summary>
        protected virtual void Serialize() { }
    }

    #if UNITY_EDITOR
    [CustomNodeEditor(typeof(BehaviourTreeNode))]
    public class BehaviourTreeNodeEditor : NodeEditor
    {
        private BehaviourTreeNode _targetNode;

        public override void OnCreate()
        {
            _targetNode = target as BehaviourTreeNode;
        }
        
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
            string[] excludes = {"m_Script", "graph", "position", "ports", "parent", "child", "children", "condition"};

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
        
        /// <summary>
        /// Returns the colour of the node.
        /// <para/>
        /// If a controller is selected when the application is playing,
        /// returns the colour for the controller's current node status.
        /// <para/>
        /// Else returns the default node colour.
        /// </summary>
        public override Color GetTint()
        {
            // Get the selected controller
            var controller = _targetNode.Graph.SelectedController;
            
            // Return the default node colour if no controller has been selected.
            if (controller == null)
            {
                return _targetNode.Graph.nodeDefault;
            }
            
            // Return the node status colour for the controller.
            switch (_targetNode.GetStatus(controller))
            {
                case BehaviourTreeNode.Status.Completed:
                    return _targetNode.Graph.nodeCompleted;
                case BehaviourTreeNode.Status.Running:
                    return _targetNode.Graph.nodeRunning;
                case BehaviourTreeNode.Status.Failed:
                    return _targetNode.Graph.nodeFailed;
                case BehaviourTreeNode.Status.Invalid:
                    return _targetNode.Graph.nodeInvalid;
                case BehaviourTreeNode.Status.Ready:
                    return _targetNode.Graph.nodeReady;
                default:
                    return _targetNode.Graph.nodeDefault;
            }
        }
    }
    #endif
}
