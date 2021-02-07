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

                DamageParams damageParams = new DamageParams();
                damageParams.DamageAmount = 15;
                damageParams.SourceStats = sourceCombatStats;
                damageParams.SourceEntity = gameObject;
                damageParams.Element = element;

                handler.ApplyDamage(ref damageParams);

                Assert.AreEqual(damageParams.DamageAmount, 15);
                handler.CombatStats = null;
            }

            {
                Element element = ScriptableObject.CreateInstance<Element>();

                CombatStats sourceCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                sourceCombatStats.DamageModifiers = new List<DamageModifier>();
                CombatStats targetCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                targetCombatStats.DamageModifiers = new List<DamageModifier>();
                handler.CombatStats = targetCombatStats;

                DamageStep damageStep = ScriptableObject.CreateInstance<DamageStep>();
                damageStep.Priority = 0;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Multiply;
                operation.Value = 2f;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.DamageStep = damageStep;
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

                DamageStep damageStep = ScriptableObject.CreateInstance<DamageStep>();
                damageStep.Priority = 0;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Add;
                operation.Value = 3f;

                FunctionOperation operation2 = new FunctionOperation();
                operation2.Operation = FunctionOperation.OperationType.Multiply;
                operation2.Value = 3f;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.DamageStep = damageStep;
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

                DamageStep damageStep = ScriptableObject.CreateInstance<DamageStep>();
                damageStep.Priority = 0;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Add;
                operation.Value = 3f;

                FunctionOperation operation2 = new FunctionOperation();
                operation2.Operation = FunctionOperation.OperationType.Multiply;
                operation2.Value = 3f;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.DamageStep = damageStep;
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

            {
                Element element = ScriptableObject.CreateInstance<Element>();

                CombatStats sourceCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                sourceCombatStats.DamageModifiers = new List<DamageModifier>();
                CombatStats targetCombatStats = ScriptableObject.CreateInstance<CombatStats>();
                targetCombatStats.DamageModifiers = new List<DamageModifier>();
                handler.CombatStats = targetCombatStats;

                DamageStep damageStep = ScriptableObject.CreateInstance<DamageStep>();
                damageStep.Priority = 0;

                DamageStep damageStep2 = ScriptableObject.CreateInstance<DamageStep>();
                damageStep2.Priority = 1;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Add;
                operation.Value = 3f;

                FunctionOperation operation2 = new FunctionOperation();
                operation2.Operation = FunctionOperation.OperationType.Multiply;
                operation2.Value = 3f;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.DamageStep = damageStep;
                modifier.Function.Operations = new FunctionOperation[1] { operation };

                ElementDamageModifier modifier2 = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier2.Element = element;
                modifier2.DamageStep = damageStep2;
                modifier2.Function.Operations = new FunctionOperation[1] { operation2 };

                targetCombatStats.DamageModifiers.Add(modifier2);
                targetCombatStats.DamageModifiers.Add(modifier);

                DamageParams damageParams = new DamageParams();
                damageParams.DamageAmount = 15;
                damageParams.SourceStats = sourceCombatStats;
                damageParams.SourceEntity = gameObject;
                damageParams.Element = element;

                handler.ApplyDamage(ref damageParams);

                Assert.AreEqual(damageParams.DamageAmount, 54);
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

                DamageStep damageStep = ScriptableObject.CreateInstance<DamageStep>();
                damageStep.Priority = 0;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.DamageStep = damageStep;
                modifier.Function.Operations = new FunctionOperation[2] { operation, operation2 };

                for (int i = 0; i < 1000; ++i)
                    targetCombatStats.DamageModifiers.Add(modifier);


                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                for (int i = 0; i < 100000; i++)
                {
                    operation.Value = i;
                    operation2.Value = i;

                    DamageParams damageParams = new DamageParams();
                    damageParams.DamageAmount = 15;
                    damageParams.SourceStats = sourceCombatStats;
                    damageParams.SourceEntity = gameObject;
                    damageParams.Element = element; // not the same element

                    handler.ApplyDamage(ref damageParams);
                }
                watch.Stop();
                Debug.Log("Test Case Runtime: " + watch.ElapsedMilliseconds + "ms");
            }

        }

        void Update()
        {
            for (int i = 0; i < 1; ++i)
            {
                new DamageParams();
            }
        }
    }
}
