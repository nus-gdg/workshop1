using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Common.Logic
{
    public class BehaviorTreeController : MonoBehaviour
    {
        [SerializeField]
        private BehaviorTree behaviorTree;
        
        [SerializeField]
        private Blackboard blackboard;
        private HashSet<Node> _runningNodes;

        private void OnEnable()
        {
            blackboard.Refresh();
            _runningNodes = new HashSet<Node>();
        }

        private void Update()
        {
            var result = behaviorTree.Evaluate(this);
            if (result == BehaviorTreeNode.Status.Completed)
            {
                _runningNodes.Clear();
            }
        }

        public object this[BlackboardKey key]
        {
            get => blackboard[key];
            set => blackboard[key] = value;
        }

        public bool TryGetValue<T>(BlackboardKey key, out T value)
        {
            return blackboard.TryGetValue(key, out value);
        }
        
        public void AddValue(BlackboardKey key, object value)
        {
            blackboard.Add(key, value);
        }

        public void RemoveValue(BlackboardKey key)
        {
            blackboard.Remove(key);
        }

        public bool IsRunningNode(BehaviorTreeNode node)
        {
            return _runningNodes.Contains(node);
        }

        public void RegisterNodeStatus(BehaviorTreeNode node, BehaviorTreeNode.Status status)
        {
            if (status == BehaviorTreeNode.Status.Running)
            {
                _runningNodes.Add(node);

            }
            else
            {
                _runningNodes.Remove(node);
            }
        }
    }
}
