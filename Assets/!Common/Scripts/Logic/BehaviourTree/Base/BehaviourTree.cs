using System;
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
        public RootNode root;

        public virtual void Load(BehaviourTreeController controller)
        {
            root.Load(controller);
        }
        
        public virtual void Unload(BehaviourTreeController controller)
        {
            root.Unload(controller);
        }

        public BehaviourTreeNode.Status Evaluate(BehaviourTreeController controller)
        {
            return root.Evaluate(controller);
        }

        public override Node AddNode(Type type)
        {
            var node = base.AddNode(type);
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
    }
    #endif
}
