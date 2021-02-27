using Common.Logic;
using UnityEngine;

namespace Logic.Entity
{
    [CreateNodeMenu("Animator/Set Trigger")]
    public class SetAnimatorTrigger : TaskNode
    {
        public BlackboardKey animator;
        public string trigger;
        public int hashedTrigger;

        protected override void Init()
        {
            hashedTrigger = Animator.StringToHash(trigger);
        }

        public override Status Evaluate(BehaviourTreeController controller)
        {
            if (!controller.TryGetValue(animator, out Animator animatorValue))
            {
                return Status.Failed;
            }
            animatorValue.SetTrigger(hashedTrigger);
            return Status.Completed;
        }
    }
}
