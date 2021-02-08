using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Testing
{
    public class TestingScript : MonoBehaviour
    {
        public bool DoStressTest = false;
        // Start is called before the first frame update
        void Start()
        {
            // Initial parameters: Fire (x2), Water (x0.5)
            var receiver = GetComponent<DamageReceiver>();
            var receiverStats = GetComponent<EntityStats>();

            {
                receiverStats.DefensiveDamageModifiers = new List<DamageModifier>();

                Element element = ScriptableObject.CreateInstance<Element>();

                DamageSource src = new DamageSource();
                src.DamageAmount = 15;
                src.SourceEntity = gameObject;
                src.Element = element;
                src.Modifiers = new List<DamageModifier>();

                DamageResult res = receiver.ApplyDamage(src);
                Assert.AreEqual(res.DamageAmount, 15);
            }

            {
                receiverStats.DefensiveDamageModifiers = new List<DamageModifier>();

                Element element = ScriptableObject.CreateInstance<Element>();

                DamageSource src = new DamageSource();
                src.DamageAmount = 15;
                src.SourceEntity = gameObject;
                src.Element = element;
                src.Modifiers = new List<DamageModifier>();

                DamageStep damageStep = ScriptableObject.CreateInstance<DamageStep>();
                damageStep.Priority = 0;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Multiply;
                operation.Value = 2f;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.DamageStep = damageStep;
                modifier.Function.Operations = new FunctionOperation[1] { operation };

                receiverStats.DefensiveDamageModifiers.Add(modifier);
                receiverStats.OffensiveDamageModifiers.Add(modifier);

                DamageResult res = receiver.ApplyDamage(src);
                Assert.AreEqual(res.DamageAmount, 30);
            }

            {
                receiverStats.DefensiveDamageModifiers = new List<DamageModifier>();

                Element element = ScriptableObject.CreateInstance<Element>();

                DamageSource src = new DamageSource();
                src.DamageAmount = 15;
                src.SourceEntity = gameObject;
                src.Element = element;
                src.Modifiers = new List<DamageModifier>();

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

                receiverStats.DefensiveDamageModifiers.Add(modifier);
                receiverStats.OffensiveDamageModifiers.Add(modifier); // does nothing

                DamageResult res = receiver.ApplyDamage(src);
                Assert.AreEqual(res.DamageAmount, 57);
            }

            {
                receiverStats.DefensiveDamageModifiers = new List<DamageModifier>();

                Element element = ScriptableObject.CreateInstance<Element>();

                DamageSource src = new DamageSource();
                src.DamageAmount = 15;
                src.SourceEntity = gameObject;
                src.Element = element;
                src.Modifiers = new List<DamageModifier>();

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

                receiverStats.DefensiveDamageModifiers.Add(modifier);
                receiverStats.DefensiveDamageModifiers.Add(modifier2);

                DamageResult res = receiver.ApplyDamage(src);
                Assert.AreEqual(res.DamageAmount, 54);
            }

            if (DoStressTest)
            {
                receiverStats.DefensiveDamageModifiers = new List<DamageModifier>();

                Element element = ScriptableObject.CreateInstance<Element>();

                DamageSource src = new DamageSource();
                src.DamageAmount = 15;
                src.SourceEntity = gameObject;
                src.Element = element;
                src.Modifiers = new List<DamageModifier>();

                DamageStep damageStep = ScriptableObject.CreateInstance<DamageStep>();
                damageStep.Priority = 0;

                FunctionOperation operation = new FunctionOperation();
                operation.Operation = FunctionOperation.OperationType.Multiply;
                operation.Value = 2f;

                ElementDamageModifier modifier = ScriptableObject.CreateInstance<ElementDamageModifier>();
                modifier.Element = element;
                modifier.DamageStep = damageStep;
                modifier.Function.Operations = new FunctionOperation[2] { operation, operation };

                for (int i = 0; i < 1000; ++i)
                    receiverStats.OffensiveDamageModifiers.Add(modifier);


                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                for (int i = 0; i < 100000; i++)
                {
                    receiver.ApplyDamage(src);
                }
                watch.Stop();
                Debug.Log("Test Case Runtime: " + watch.ElapsedMilliseconds + "ms");
            }
        }

        void Update()
        {
            for (int i = 0; i < 1; ++i)
            {
                // new DamageParams();
            }
        }
    }
}
