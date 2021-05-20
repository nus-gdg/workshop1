using UnityEngine;

namespace Project.Views.World.Events
{
    [CreateAssetMenu(fileName = "ConditionalGameAction", menuName = "ScriptableObjects/GameAction/ConditionalGameAction", order = 1)]
    public class ConditionalGameAction : GameAction
    {
        GameCondition Condition;
        GameAction Action;

        public override Status Evaluate(GameContext context, WorldView view)
        {
            if (Condition.Evaluate(context, view) == GameCondition.Result.True)
            {
                return Action.Evaluate(context, view);
            }
            return Status.Fail;
        }
    }
}
