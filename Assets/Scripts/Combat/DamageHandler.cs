using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Combat
{

    public class DamageHandler : MonoBehaviour
    {
        public CombatStats CombatStats;
        public void ApplyDamage(ref DamageParams dmgParams)
        {
            dmgParams.TargetEntity = gameObject;
            dmgParams.TargetStats = CombatStats;

            Assert.IsNotNull(dmgParams.SourceEntity, "DamageHandler.ApplyDamage expects DamageParams SourceEntity to be not null");
            Assert.IsNotNull(dmgParams.SourceStats, "DamageHandler.ApplyDamage expects DamageParams SourceStats to be not null");
            Assert.IsNotNull(dmgParams.TargetEntity, "DamageHandler.ApplyDamage expects DamageParams TargetEntity to be not null");
            Assert.IsNotNull(dmgParams.TargetStats, "DamageHandler.ApplyDamage expects DamageParams TargetStats to be not null");

            // we need to sort this...
            foreach (DamageModifier modifier in dmgParams.SourceStats.DamageModifiers)
            {
                modifier.Evaluate(ref dmgParams);
            }

            foreach (DamageModifier modifier in dmgParams.TargetStats.DamageModifiers)
            {
                modifier.Evaluate(ref dmgParams);
            }
        }
    }

}
