using System;
using UnityEngine;
using XNode;
using XNode.NodeGroups;
#if UNITY_EDITOR
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
        public override string GetNodeMenuName(Type type)
        {
            if (!typeof(BehaviourTreeNode).IsAssignableFrom(type)
                && !typeof(NodeGroup).IsAssignableFrom(type))
            {
                return null;
            }
            return base.GetNodeMenuName(type);
        }
        
        public override void OnWindowFocusLost()
        {
            base.OnWindowFocusLost();
            target.FixMissingScripts();
        }
    }
    #endif
}
