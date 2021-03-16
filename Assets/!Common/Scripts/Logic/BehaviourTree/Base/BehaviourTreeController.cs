using UnityEngine;

namespace Common.Logic
{
    public class BehaviourTreeController : MonoBehaviour
    {
        [SerializeField]
        private BehaviourTree behaviourTree;
        public BehaviourTree BehaviourTree => behaviourTree;

        [SerializeField]
        private Blackboard blackboard;
        public Blackboard Blackboard => blackboard;

        private void OnEnable()
        {
            blackboard.Refresh();
            behaviourTree.LoadController(this);
        }

        private void OnDisable()
        {
            behaviourTree.ClearController(this);
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
                behaviourTree.LoadController(this);
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
    }
}
