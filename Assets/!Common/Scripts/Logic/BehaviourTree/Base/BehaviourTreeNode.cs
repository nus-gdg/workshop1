using XNode;

namespace Common.Logic
{
    public abstract class BehaviourTreeNode : Node
    {
        public enum Status
        {
            Ready, Completed, Running, Failed, 
        }

        public BehaviourTree Graph => graph as BehaviourTree;
        public abstract Status Evaluate(BehaviourTreeController controller);

        public override object GetValue(NodePort nodePort)
        {
            return this;
        }
    }
}
