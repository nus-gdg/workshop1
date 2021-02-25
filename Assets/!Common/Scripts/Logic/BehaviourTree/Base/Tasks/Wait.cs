using System;
using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Behaviour Tree/Tasks/Wait", -100)]
    public class Wait : TaskNode
    {
        public float time;

        [Header("Input")]
        public BlackboardKey timer;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.IsRunningNode(this))
            {
                controller[timer] = new BehaviourTreeTimer(time);
                return Status.Running;
            }

            if (!controller.TryGetValue(timer, out BehaviourTreeTimer timerValue))
            {
                throw new InvalidOperationException("Controller should have a timer at this point.");
            }

            timerValue.Tick();
            if (timerValue.IsCompleted())
            {
                controller.RemoveValue(timer);
                return Status.Completed;
            }
            return Status.Running;
        }
    }
}
