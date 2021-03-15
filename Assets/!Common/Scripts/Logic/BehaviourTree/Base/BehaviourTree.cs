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
        public Dictionary<BehaviourTreeController, HashSet<Monitor>> monitorsByController =
            new Dictionary<BehaviourTreeController, HashSet<Monitor>>();

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
        /// Adds the controller to the behaviour tree.
        /// </summary>
        public virtual void LoadController(BehaviourTreeController controller)
        {
            root.LoadController(controller);
            monitorsByController[controller] = new HashSet<Monitor>();
        }

        /// <summary>
        /// Removes the controller from the behaviour tree.
        /// </summary>
        public virtual void ClearController(BehaviourTreeController controller)
        {
            root.ClearController(controller);
            monitorsByController.Remove(controller);
        }

        /// <summary>
        /// Ticks the behaviour tree.
        /// Monitored nodes are ticked before other nodes.
        /// </summary>
        public BehaviourTreeNode.Status Tick(BehaviourTreeController controller)
        {
            TickMonitors(controller);
            return root.Tick(controller);
        }

        private void TickMonitors(BehaviourTreeController controller)
        {
            foreach (var monitor in monitorsByController[controller])
            {
                var result = monitor.TickCondition(controller);
                if (result == BehaviourTreeNode.Status.Completed)
                {
                    LoadController(controller);
                }
            }
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

        [MenuItem("CONTEXT/BehaviourTree/Fix")]
        public static void FixMissingScripts(MenuCommand command)
        {
            ((BehaviourTree)command.context).FixMissingScripts();
        }

        public override string GetNodeMenuName(Type type)
        {
            if (!typeof(BehaviourTreeNode).IsAssignableFrom(type)
                && !typeof(NodeGroup).IsAssignableFrom(type))
            {
                return null;
            }
            return base.GetNodeMenuName(type);
        }

        public override void OnOpen()
        {
            if (_behaviourTree == null || _behaviourTree.root == null)
            {
                return;
            }
            NodeEditorWindow.current.SelectNode(_behaviourTree.root, false);
            NodeEditorWindow.current.Home();
        }
        
        public override void OnGUI()
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }

            NodeEditorWindow.current.Repaint();

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
