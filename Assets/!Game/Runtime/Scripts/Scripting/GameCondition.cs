using UnityEngine;

public abstract class GameCondition : ScriptableObject
{
    public enum Result
    {
        True,
        False
    }
    public abstract Result Evaluate(GameContext context);
}
