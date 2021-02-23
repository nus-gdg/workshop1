using UnityEngine;

namespace Common.Logic
{
    public abstract class TaskNode : BehaviorTreeNode
    {
        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        [SerializeField]
        protected BehaviorTreeNode parent;
    }
}
