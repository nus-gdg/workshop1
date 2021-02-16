using Core.Events;
using System;

namespace Experimental
{
    [System.Serializable]
    public abstract class GameCondition
    {
        public abstract bool Evaluate(GameContext context);

    }
}
