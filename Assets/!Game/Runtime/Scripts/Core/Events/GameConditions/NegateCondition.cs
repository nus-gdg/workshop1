using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;

namespace Experimental
{

    public class NegateCondition : GameCondition
    {
        [SerializeReference]
        [SubclassSelectorAttribute(typeof(GameCondition))]
        GameCondition Condition;

        public override bool Evaluate(GameContext context)
        {
            return Condition != null ? !Condition.Evaluate(context) : false;
        }
    }

}
