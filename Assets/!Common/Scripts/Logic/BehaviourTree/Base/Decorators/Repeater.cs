using System.Collections.Generic;

namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Decorator/Repeater", -100)]
    public class Repeater : DecoratorNode
    {
        public int count;
        
        private Dictionary<BehaviourTreeController, int> _countsOfControllers =
            new Dictionary<BehaviourTreeController, int>();

        public override void Enter(BehaviourTreeController controller)
        {
            _countsOfControllers[controller] = count;
        }

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (_countsOfControllers[controller] <= 0)
            {
                return Status.Completed;
            }

            if (child == null)
            {
                return Status.Running;
            }
            
            var result = child.Tick(controller);
            if (result != Status.Running)
            {
                _countsOfControllers[controller]--;
            }

            return Status.Running;
        }
    }
}
