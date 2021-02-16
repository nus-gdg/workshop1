using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Experimental;
using Core.Events;
using Core;

public class IsPlayerCondition : GameCondition
{
    public override bool Evaluate(GameContext context)
    {
        return context.ContextEntity == Game.Instance.World.Player.gameObject;
    }
}
