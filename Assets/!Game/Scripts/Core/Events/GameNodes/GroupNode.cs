using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;

namespace Experimental
{
    [Serializable]
    public class GroupNode : GameNode
    {
        [SerializeReference]
        [SubclassSelectorAttribute(typeof(GameNode))]
        List<GameNode> Nodes = new List<GameNode>();

        public GroupNode()
        {
            NodeName = "Group Node";
        }

        public override void Evaluate(GameContext context)
        {
            foreach (GameNode node in Nodes)
            {
#if UNITY_EDITOR
                if (node == null)
                {
                    Debug.LogAssertion($"Group Node { NodeName } has null child, please remove it");
                    continue;
                }
#endif
                node.Evaluate(context);
            }
        }
    }
}

