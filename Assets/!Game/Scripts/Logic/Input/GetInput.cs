using System;
using Common.Logic;
using Core;
using UnityEngine;

namespace Entity.Logic
{
    [CreateNodeMenu("Game/Input/Get")]
    public class GetInput : TaskNode
    {
        public enum InputAction
        {
            Move, Aim, AttackDown, Attack,
        }

        public BlackboardKey key;

        [NodeEnum]
        public InputAction action;

        public override Status Evaluate(BehaviourTreeController controller)
        {
            switch (action)
            {
                case InputAction.Move:
                    controller[key] = Game.Instance.Input.Player.Move.ReadValue<Vector2>();
                    break;
                case InputAction.Aim:
                    controller[key] = Game.Instance.Input.Player.Aim.ReadValue<Vector2>();
                    break;
                case InputAction.AttackDown:
                    controller[key] = Game.Instance.Input.Player.Action1.triggered;
                    break;
                case InputAction.Attack:
                    controller[key] = Game.Instance.Input.Player.Action1.ReadValue<float>();
                    break;
                default:
                    throw new InvalidOperationException("Invalid input action selected");
            }
            return Status.Completed;
        }
    }
}
