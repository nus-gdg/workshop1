using UnityEngine;

namespace Core.Events
{
    public abstract class GroupGameCondition : GameCondition
    {
        public GameCondition[] Conditions;
    }

    [CreateAssetMenu(fileName = "OrGameCondition", menuName = "ScriptableObjects/GameCondition/OrGameCondition", order = 1)]
    public class OrGameCondition : GroupGameCondition
    {
        public override Result Evaluate(GameContext context)
        {
            foreach (GameCondition condition in Conditions)
            {
                if (condition.Evaluate(context) == Result.True)
                {
                    return Result.True;
                }
            }
            return Result.False;
        }
    }

    [CreateAssetMenu(fileName = "AndGameCondition", menuName = "ScriptableObjects/GameCondition/AndGameCondition", order = 1)]
    public class AndGameCondition : GroupGameCondition
    {
        public override Result Evaluate(GameContext context)
        {
            foreach (GameCondition condition in Conditions)
            {
                if (condition.Evaluate(context) == Result.False)
                {
                    return Result.False;
                }
            }
            return Result.True;
        }
    }
}