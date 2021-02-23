using System;
using UnityEngine;

namespace Common.Logic
{
    [CreateNodeMenu("Tasks/Wait", -80)]
    public class Wait : TaskNode
    {
        public float time;

        [Header("Input")]
        public BlackboardKey timer;

        public override Status Evaluate(BehaviorTreeController controller)
        {
            if (!controller.IsRunningNode(this))
            {
                controller[timer] = new BehaviorTreeTimer(time);
                return Status.Running;
            }

            if (!controller.TryGetValue(timer, out BehaviorTreeTimer timerValue))
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
