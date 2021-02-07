using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Testing
{
    public class TestingScript : MonoBehaviour
    {
        DamageHandler handler;

        // Start is called before the first frame update
        void Start()
        {
            // Initial parameters: Fire (x2), Water (x0.5)
            handler = GetComponent<DamageHandler>();
            {
                Element element = ScriptableObject.CreateInstance<Element>();

                CombatStats sourceCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                sourceCombatStats.DamageModifiers = new List<DamageModifier>();
                CombatStats targetCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                targetCombatStats.DamageModifiers = new List<DamageModifier>();
                handler.CombatStats = targetCombatStats;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Multiply;
                operation.Value = 2f;
                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.Function.Operations = new FunctionOperation[1] { operation };

                targetCombatStats.DamageModifiers.Add(modifier);

                DamageParams damageParams = new DamageParams();
                damageParams.DamageAmount = 15;
                damageParams.SourceStats = sourceCombatStats;
                damageParams.SourceEntity = gameObject;
                damageParams.Element = element;

                handler.ApplyDamage(ref damageParams);

                Assert.AreEqual(damageParams.DamageAmount, 30);
                handler.CombatStats = null;
            }

            {
                Element element = ScriptableObject.CreateInstance<Element>();

                CombatStats sourceCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                sourceCombatStats.DamageModifiers = new List<DamageModifier>();
                CombatStats targetCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                targetCombatStats.DamageModifiers = new List<DamageModifier>();
                handler.CombatStats = targetCombatStats;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Add;
                operation.Value = 3f;

                FunctionOperation operation2 = new FunctionOperation();
                operation2.Operation = FunctionOperation.OperationType.Multiply;
                operation2.Value = 3f;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.Function.Operations = new FunctionOperation[3] { operation, operation2, operation }; // ((value + 3) * 3) + 3

                targetCombatStats.DamageModifiers.Add(modifier);

                DamageParams damageParams = new DamageParams();
                damageParams.DamageAmount = 15;
                damageParams.SourceStats = sourceCombatStats;
                damageParams.SourceEntity = gameObject;
                damageParams.Element = element;

                handler.ApplyDamage(ref damageParams);

                Assert.AreEqual(damageParams.DamageAmount, 57);
                handler.CombatStats = null;
            }

            {
                Element element = ScriptableObject.CreateInstance<Element>();
                Element element2 = ScriptableObject.CreateInstance<Element>();

                CombatStats sourceCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                sourceCombatStats.DamageModifiers = new List<DamageModifier>();
                CombatStats targetCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                targetCombatStats.DamageModifiers = new List<DamageModifier>();
                handler.CombatStats = targetCombatStats;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Add;
                operation.Value = 3f;

                FunctionOperation operation2 = new FunctionOperation();
                operation2.Operation = FunctionOperation.OperationType.Multiply;
                operation2.Value = 3f;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.Function.Operations = new FunctionOperation[2] { operation, operation2 };

                targetCombatStats.DamageModifiers.Add(modifier);

                DamageParams damageParams = new DamageParams();
                damageParams.DamageAmount = 15;
                damageParams.SourceStats = sourceCombatStats;
                damageParams.SourceEntity = gameObject;
                damageParams.Element = element2; // not the same element

                handler.ApplyDamage(ref damageParams);

                Assert.AreEqual(damageParams.DamageAmount, 15);
                handler.CombatStats = null;
            }
        }

    }
}
