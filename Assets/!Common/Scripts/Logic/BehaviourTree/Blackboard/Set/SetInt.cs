namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Set/int", -100)]
    public class SetInt : SetNode<int>
    {
        public bool relative;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!relative || !controller.TryGetValue(key, out int internalValue))
            {
                controller[key] = value;
            }
            else
            {
                controller[key] = internalValue + value;
            }
            return Status.Completed;
        }
    }
}
