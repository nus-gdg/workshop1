using System.Collections;
using System.Collections.Generic;
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
