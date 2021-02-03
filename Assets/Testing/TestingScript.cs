using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Combat.ElementalDamageCalculator;

namespace Testing
{
    public class TestingScript : MonoBehaviour
    {
        ResistanceHandler handler;

        [SerializeField]
        Element element1;
        [SerializeField]
        Element element2;
        [SerializeField]
        Element element3;
        [SerializeField]
        ResistanceMapping resistanceMapping1;
        [SerializeField]
        ResistanceMapping resistanceMapping2;
        [SerializeField]
        ResistanceMapping resistanceMapping3;

        int damage = 10;

        // Start is called before the first frame update
        void Start()
        {
            // Initial parameters: Fire (x2), Water (x0.5)
            handler = GetComponent<ResistanceHandler>();

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            // Test block 1: Basic functionality
            Debug.Log("Test Case 1a: " +
                (CalculateDamage(element1, damage, handler) == 20).ToString());
            Debug.Log("Test Case 1b: " +
                (CalculateDamage(element2, damage, handler) == 5).ToString());
            Debug.Log("Test Case 1c: " +
                (CalculateDamage(element3, damage, handler) == 10).ToString());
            Debug.Log("Test Case 1 Runtime: " + watch.ElapsedMilliseconds + "ms");

            // Test block 2: Updating resistances dynamically
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            resistanceMapping1.hyperparameters = new float[1] { 2f };
            resistanceMapping2.hyperparameters = new float[1] { 4f };
            resistanceMapping3.element = element3;
            resistanceMapping3.hyperparameters = new float[1] { 0.5f };
            handler.AddResistance(resistanceMapping1);
            handler.AddResistance(resistanceMapping2);
            handler.AddResistance(resistanceMapping3);

            Debug.Log("Test Case 2a: " +
                (CalculateDamage(element1, damage, handler) == 20).ToString());
            Debug.Log("Test Case 2b: " +
                (CalculateDamage(element2, damage, handler) == 40).ToString());
            Debug.Log("Test Case 2c: " +
                (CalculateDamage(element3, damage, handler) == 5).ToString());
            watch.Stop();
            Debug.Log("Test Case 2 Runtime: " + watch.ElapsedMilliseconds + "ms");

            // Test block 3: Stress test
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 0; i < 100000; i++)
            {
                resistanceMapping1.hyperparameters = new float[1] { i };
                handler.AddResistance(resistanceMapping1);
                CalculateDamage(element1, damage, handler);
            }
            watch.Stop();
            Debug.Log("Test Case 3 Runtime: " + watch.ElapsedMilliseconds + "ms");
        }

        private void Update()
        {
            // Test block 4: Memory leak test
            resistanceMapping1.hyperparameters = new float[1] { 1 };
            handler.AddResistance(resistanceMapping1);
            CalculateDamage(element1, damage, handler);
        }
    }
}
