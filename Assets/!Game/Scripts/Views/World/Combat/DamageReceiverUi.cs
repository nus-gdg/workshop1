using System;
using System.Collections.Generic;
using System.Linq;
using Project.Models.Combat;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Views.Combat
{
    public struct DamageResult
    {
        public int DamageAmount;
    }

    [RequireComponent(typeof(HealthUi))]
    public class DamageReceiverUi : MonoBehaviour
    {
        private EntityStats entityStats;
        private HealthUi entityHealth;

        void Awake()
        {
            entityStats = GetComponent<EntityStats>();
            entityHealth = GetComponent<HealthUi>();
        }

        public DamageResult ApplyDamage(DamageSource damageSource)
        {
            IEnumerable<DamageModifier> modifiers = damageSource.Modifiers;
            if (entityStats != null)
                modifiers = modifiers.Concat(entityStats.DefensiveDamageModifiers);

            IEnumerable<DamageModifier> modifierQuery = from modifier in modifiers
                                                        orderby modifier.DamageStep ? modifier.DamageStep?.Priority : System.Int32.MaxValue
                                                        select modifier;

            DamageModifierRuntimeParams dmgModRuntimeParams = new DamageModifierRuntimeParams();
            dmgModRuntimeParams.Element = damageSource.Element;
            dmgModRuntimeParams.DamageAmount = damageSource.DamageAmount;
            dmgModRuntimeParams.SourceEntity = damageSource.SourceEntity;
            dmgModRuntimeParams.TargetEntity = gameObject;

            foreach (DamageModifier modifier in modifierQuery)
            {
                Assert.IsNotNull(modifier.DamageStep, "DamageReceiver.ApplyDamage expects DamageModifier damage step to be not null");
                modifier.Evaluate(ref dmgModRuntimeParams);
            }

            Debug.Log(String.Format("DamageReceive.ApplyDamage, Damage Result: {0}", dmgModRuntimeParams.DamageAmount));
            entityHealth.Damage(dmgModRuntimeParams.DamageAmount);
            return new DamageResult { DamageAmount = dmgModRuntimeParams.DamageAmount };
        }
    }
}
