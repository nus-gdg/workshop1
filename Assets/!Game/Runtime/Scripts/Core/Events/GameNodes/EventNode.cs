using System;
using UnityEngine;
using UnityEngine.Events;
using Core.Events;

namespace Experimental
{
    public class EventNode : GameNode
    {
        public GameEvent GameEvent;

        public EventNode()
        {
            NodeName = "Event Node";
        }

        public override void Evaluate(GameContext context)
        {
            GameEvent.Invoke(context);
        }
    }
}
