namespace Common.Logic
{
    [CreateNodeMenu("Blackboard/Set/float", -100)]
    public class SetFloat : SetNode<float>
    {
        public bool relative;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!relative || !controller.TryGetValue(key, out float internalValue))
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
