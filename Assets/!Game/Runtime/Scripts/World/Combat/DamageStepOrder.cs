﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

namespace Combat
{
    [CreateAssetMenu(fileName = "DamageStepOrder", menuName = "ScriptableObjects/Combat/DamageStepOrder")]
    public class DamageStepOrder : ScriptableObject
    {
        public List<DamageStep> DamageSteps;

        void OnEnable()
        {
            UpdatePriorities();
        }

        void UpdatePriorities()
        {
            DamageSteps.RemoveAll(x => x == null);
            bool isUnique = DamageSteps.Distinct().Count() == DamageSteps.Count();
            Assert.IsTrue(isUnique, "DamageStepOrder.UpdatePriorities expect Damage Steps to be unique");

            int priority = 0;
            foreach (DamageStep damageStep in DamageSteps)
            {
                damageStep.Priority = priority;
                ++priority;
            }
            Debug.Log("DamageStepOrder Priorities Updated");
        }
    }

}
