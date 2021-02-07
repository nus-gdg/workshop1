using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

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

            IEnumerable<DamageModifier> modifiers = dmgParams.SourceStats.DamageModifiers.Concat(dmgParams.TargetStats.DamageModifiers);
            IEnumerable<DamageModifier> modifierQuery = from modifier in modifiers
                                                        orderby modifier.DamageStep ? modifier.DamageStep?.Priority : System.Int32.MaxValue
                                                        select modifier;

            foreach (DamageModifier modifier in modifierQuery)
            {
                Assert.IsNotNull(modifier.DamageStep, "DamageHandler.ApplyDamage expects DamageModifier damage step to be not null");
                // we can log here too
                modifier.Evaluate(ref dmgParams);
            }
        }
    }

}
