using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionalGameAction", menuName = "ScriptableObjects/GameAction/ConditionalGameAction", order = 1)]
public class ConditionalGameAction : GameAction
{
    GameCondition Condition;
    GameAction Action;
    public override Status Evaluate(GameContext context)
    {
        if (Condition.Evaluate(context) == GameCondition.Result.True)
        {
            return Action.Evaluate(context);
        }
        return Status.Invalid;
    }
}
