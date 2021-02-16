﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;

namespace Experimental
{
    public class GameNodeComponent : MonoBehaviour
    {
        [SerializeReference]
        [SubclassSelectorAttribute(typeof(GameNode))]
        public GameNode GameNode;
        public void Evaluate(GameContext context)
        {
            if (GameNode == null)
                Debug.LogAssertion($"GameNodeComponent on { gameObject } has null GameNode");

            GameNode.Evaluate(context);
        }
    }
}

