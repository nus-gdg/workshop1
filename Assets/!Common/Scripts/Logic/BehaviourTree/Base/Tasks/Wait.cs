using System.Collections.Generic;
using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Tasks/Wait", -100)]
    public class Wait : TaskNode
    {
        public float time;
        
        private Dictionary<BehaviourTreeController, float> _timersOfControllers =
            new Dictionary<BehaviourTreeController, float>();

        public override void Enter(BehaviourTreeController controller)
        {
            _timersOfControllers[controller] = Time.time + time;
        }

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (_timersOfControllers[controller] > Time.time)
            {
                return Status.Running;
            }
            return Status.Completed;
        }
    }
}
