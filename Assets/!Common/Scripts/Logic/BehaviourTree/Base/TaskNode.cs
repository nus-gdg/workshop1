using UnityEngine;

namespace Common.Logic
{
    public abstract class TaskNode : BehaviourTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [SerializeField]
        protected BehaviourTreeNode parent;
    }
}
