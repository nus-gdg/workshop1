using System.Collections.Generic;
using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Decorator/Monitor", -100)]
    public class Monitor : DecoratorNode
    {
        public float frequency;

        private Dictionary<BehaviourTreeController, float> _timersOfControllers =
            new Dictionary<BehaviourTreeController, float>();

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (child == null)
            {
                return Status.Completed;
            }
            return child.Tick(controller);
        }

        public override void Exit(BehaviourTreeController controller)
        {
            if (child.IsStatus(controller, Status.Failed))
            {
                _timersOfControllers[controller] = Time.time + frequency;
                Graph.AddMonitor(controller, this);
            }
        }

        public Status Refresh(BehaviourTreeController controller)
        {
            if (_timersOfControllers[controller] > Time.time)
            {
                return GetStatus(controller);
            }

            var result = Evaluate(controller);
            if (result != Status.Running)
            {
                _timersOfControllers[controller] += frequency;
            }

            return result;
        }
    }
}
