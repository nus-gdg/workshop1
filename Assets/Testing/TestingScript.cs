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
        Element element;
        int damage = 10;

        // Start is called before the first frame update
        void Start()
        {
            handler = GetComponent<ResistanceHandler>();
            Debug.Log(CalculateDamage(element, damage, handler));
        }
    }
}
