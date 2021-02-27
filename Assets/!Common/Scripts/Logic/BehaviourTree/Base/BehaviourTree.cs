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
    public class BehaviourTree : NodeGraph
    {
        public CompositeNode root;

        public BehaviourTreeNode.Status Evaluate(BehaviourTreeController controller)
        {
            var result = root.Evaluate(controller);
            controller.RegisterNodeStatus(root, result);
            return result;
        }
    }

    #if UNITY_EDITOR
    [CustomNodeGraphEditor(typeof(BehaviourTree))]
    public class BehaviourTreeEditor : NodeGraphEditor
    {
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
            var graph = target as BehaviourTree;
            if (graph == null || graph.root == null)
            {
                return;
            }
            NodeEditorWindow.current.SelectNode(graph.root, false);
            NodeEditorWindow.current.Home();
        }
    }
    #endif
}
