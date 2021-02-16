using Core.Events;
using System;
using UnityEngine;

namespace Experimental
{
    [Serializable]
    public abstract class GameCondition
    {
        public abstract bool Evaluate(GameContext context);
    }
}
