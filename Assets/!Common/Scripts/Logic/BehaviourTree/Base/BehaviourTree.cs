using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNode.NodeGroups;
#if UNITY_EDITOR
using UnityEditor;
using XNodeEditor;
#endif

namespace Common.Logic
{
    [CreateAssetMenu(fileName = "Behaviour Tree", menuName = "Behaviour Tree")]
    [RequireNode(typeof(RootNode))]
    public class BehaviourTree : NodeGraph
    {
        /// <summary>
        /// The root node of the behaviour tree.
        /// </summary>
        public RootNode root;
        
        /// <summary>
        /// The list of monitored nodes for each controller using this behaviour tree.
        /// </summary>
        public Dictionary<BehaviourTreeController, List<Monitor>> monitorsByController =
            new Dictionary<BehaviourTreeController, List<Monitor>>();

#if UNITY_EDITOR

        // Graph editor colour settings
        public Color nodeCompleted = new Color32(45, 76, 37, 255);
        public Color nodeRunning = new Color32(76, 66, 33, 255);
        public Color nodeFailed = new Color32(76, 39, 28, 255);
        public Color nodeInvalid = new Color32(45, 53, 76, 255);
        public Color nodeReady = new Color32(76, 41, 72, 255);
        public Color nodeDefault = new Color32(90, 97, 105, 255);

        /// <summary>
        /// Highlight the status of this controller in the graph editor.
        /// </summary>
        [NonSerialized]
        public BehaviourTreeController SelectedController;
#endif

        /// <summary>
        /// Ticks the behaviour tree.
        /// <para/>
        /// The following is the sequence of events that occur every tick:
        /// Monitors -> Services -> Nodes.
        /// </summary>
        public void Tick(BehaviourTreeController controller)
        {
            TickMonitors(controller);
            TickNodes(controller);
        }

        private void TickMonitors(BehaviourTreeController controller)
        {
            var monitors = monitorsByController[controller];
            for (int i = 0; i < monitors.Count; i++)
            {
                // Reset when a monitor returns a completed status.
                var result = monitors[i].Refresh(controller);
                if (result == BehaviourTreeNode.Status.Completed)
                {
                    ResetController(controller);
                    break;
                }
            }
        }

        private void TickNodes(BehaviourTreeController controller)
        {
            root.Tick(controller);
        }

        /// <summary>
        /// Adds a monitor node to the behaviour tree asset.
        /// </summary>
        public void AddMonitor(BehaviourTreeController controller, Monitor monitor)
        {
            monitorsByController[controller].Add(monitor);
        }
        
        /// <summary>
        /// Removes a monitor node from the behaviour tree asset.
        /// </summary>
        public void RemoveMonitor(BehaviourTreeController controller, Monitor monitor)
        {
            monitorsByController.Remove(controller);
        }

        /// <summary>
        /// Adds a node to the behaviour tree asset.
        /// </summary>
        public override Node AddNode(Type type)
        {
            var node = base.AddNode(type);
            
            // Automatically create and reference the root node
            if (type == typeof(RootNode))
            {
                root = node as RootNode;
            }
            return node;
        }

        /// <summary>
        /// Resets the node status for the given behaviour tree controller.
        /// </summary>
        public virtual void ResetController(BehaviourTreeController controller)
        {
            root.ResetController(controller);
            monitorsByController[controller] = new List<Monitor>();
        }

        /// <summary>
        /// Resets the node status for all controllers running this behaviour tree.
        /// </summary>
        public virtual void ResetControllers()
        {
            root.ResetControllers();
            foreach (var controller in monitorsByController.Keys)
            {
                monitorsByController[controller] = new List<Monitor>();
            }
        }

        /// <summary>
        /// Removes the node status for the given behaviour tree controller.
        /// </summary>
        public virtual void RemoveController(BehaviourTreeController controller)
        {
            root.RemoveController(controller);
            monitorsByController.Remove(controller);
        }

        /// <summary>
        /// Refreshes the graph editor after making changes.
        /// </summary>
        private void OnValidate()
        {
            ResetControllers();
        }
    }

    #if UNITY_EDITOR
    [CustomNodeGraphEditor(typeof(BehaviourTree))]
    public class BehaviourTreeEditor : NodeGraphEditor
    {
        private BehaviourTree _behaviourTree;

        public override void OnCreate()
        {
            _behaviourTree = target as BehaviourTree;
        }

        /// <summary>
        /// Creates an inspector menu option to remove nodes with missing scripts in the behaviour tree asset.
        /// </summary>
        [MenuItem("CONTEXT/BehaviourTree/Fix")]
        public static void FixMissingScripts(MenuCommand command)
        {
            ((BehaviourTree)command.context).FixMissingScripts();
        }

        /// <summary>
        /// Filters nodes that are allowed in the node menu.
        /// </summary>
        public override string GetNodeMenuName(Type type)
        {
            // Skip any nodes that do not derive from BehaviourTreeNode or NodeGroup
            if (!typeof(BehaviourTreeNode).IsAssignableFrom(type)
                && !typeof(NodeGroup).IsAssignableFrom(type))
            {
                return null;
            }
            return base.GetNodeMenuName(type);
        }

        /// <summary>
        /// Initializes the behaviour tree graph when it is opened.
        /// </summary>
        public override void OnOpen()
        {
            // Do nothing if a tree is not open or does not have a root node (due to legacy behaviour trees) 
            if (_behaviourTree == null || _behaviourTree.root == null)
            {
                return;
            }
            // Centre the graph on the root node
            NodeEditorWindow.current.SelectNode(_behaviourTree.root, false);
            NodeEditorWindow.current.Home();
        }
        
        /// <summary>
        /// Updates the behaviour tree graph.
        /// </summary>
        public override void OnGUI()
        {
            // Do nothing if the application is not playing
            if (!EditorApplication.isPlaying)
            {
                return;
            }

            // Updates the appearance of the nodes in the graph.
            // If a controller is selected, update the node colours.
            NodeEditorWindow.current.Repaint();

            // If a controller is selected, store its reference temporarily.
            var transform = Selection.activeTransform;
            if (transform == null)
            {
                return;
            }
            var controller = transform.GetComponent<BehaviourTreeController>();
            if (controller == null)
            {
                return;
            }
            _behaviourTree.SelectedController = controller;
        }
    }
    #endif
}
