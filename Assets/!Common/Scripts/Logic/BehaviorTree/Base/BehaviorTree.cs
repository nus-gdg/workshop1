using System;
using UnityEngine;
using XNode;
using XNode.NodeGroups;
#if UNITY_EDITOR
using XNodeEditor;
#endif

namespace Common.Logic
{
    [CreateAssetMenu(fileName = "Behavior Tree", menuName = "Behavior Tree")]
    public class BehaviorTree : NodeGraph
    {
        public CompositeNode root;

        public BehaviorTreeNode.Status Evaluate(BehaviorTreeController controller)
        {
            var result = root.Evaluate(controller);
            controller.RegisterNodeStatus(root, result);
            return result;
        }
    }

    #if UNITY_EDITOR
    [CustomNodeGraphEditor(typeof(BehaviorTree))]
    public class BehaviorTreeEditor : NodeGraphEditor
    {
        public override string GetNodeMenuName(Type type)
        {
            if (!typeof(BehaviorTreeNode).IsAssignableFrom(type)
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
