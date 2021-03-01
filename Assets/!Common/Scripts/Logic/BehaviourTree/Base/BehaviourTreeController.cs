using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Common.Logic
{
    public class BehaviourTreeController : MonoBehaviour
    {
        [SerializeField]
        private BehaviourTree behaviourTree;
        
        [SerializeField]
        private Blackboard blackboard;
        private HashSet<Node> _runningNodes;

        private void OnEnable()
        {
            blackboard.Refresh();
            behaviourTree.Load(this);
        }

        private void OnDisable()
        {
            behaviourTree.Unload(this);
        }

        private void Update()
        {
            behaviourTree.Tick(this);
        }

        private void OnValidate()
        {
            blackboard.Refresh();
            if (Application.isPlaying)
            {
                behaviourTree.Load(this);
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
        
        public bool ContainsKey(BlackboardKey key)
        {
            return blackboard.ContainsKey(key);
        }

        public bool CompareValue(BlackboardKey key, object value)
        {
            if (!blackboard.TryGetValue(key, out object internalValue))
            {
                return false;
            }
            return value.Equals(internalValue);
        }

        public bool IsRunningNode(BehaviourTreeNode node)
        {
            return _runningNodes.Contains(node);
        }

        public void RegisterNodeStatus(BehaviourTreeNode node, BehaviourTreeNode.Status status)
        {
            if (status == BehaviourTreeNode.Status.Running)
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
