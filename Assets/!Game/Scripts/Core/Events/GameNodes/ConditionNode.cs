using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Events;

namespace Experimental
{
    public class ConditionNode : GameNode
    {
        public enum ConditionEvaluation
        {
            AND,
            OR,
        }

        public ConditionEvaluation Evaluation;

        [SerializeReference]
        [SubclassSelectorAttribute(typeof(GameCondition))]
        List<GameCondition> Conditions = new List<GameCondition>();

        [SerializeReference]
        [SubclassSelectorAttribute(typeof(GameNode))]
        GameNode NodeToEvaluate;

        public ConditionNode()
        {
            NodeName = "Condition Node";
        }

        public override void Evaluate(GameContext context)
        {
            if (Conditions.RemoveAll(x => x == null) > 0)
            {
                Debug.LogAssertion($"Condition Node { NodeName } has null condition, please remove it");
                return;
            }

            if (Conditions.Count == 0)
            {
                Debug.LogAssertion($"Condition Node { NodeName } has no non-null condition, please address it");
                return;
            }

            bool result = false;
            switch (Evaluation)
            {
                case ConditionEvaluation.AND:
                    result = EvaluateAnd(context);
                    break;
                case ConditionEvaluation.OR:
                    result = EvaluateOR(context);
                    break;
            }

            if (result)
            {
                NodeToEvaluate.Evaluate(context);
            }
        }

        public bool EvaluateAnd(GameContext context)
        {
            foreach (GameCondition condition in Conditions)
            {
                if (!condition.Evaluate(context))
                {
                    return false;
                }
            }
            return true;
        }

        public bool EvaluateOR(GameContext context)
        {
            foreach (GameCondition condition in Conditions)
            {
                if (condition.Evaluate(context))
                {
                    return true;
                }
            }
            return false;
        }
    }

}

