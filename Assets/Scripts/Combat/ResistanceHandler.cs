using System.Collections.Generic;
using UnityEngine;
using static Combat.ElementalDamageCalculator;

namespace Combat
{
    /// <summary>
    /// A class that handles the stats of a player, namely, how it should 
    /// handle incoming damage by elemental type. 
    /// </summary>
    public class ResistanceHandler : MonoBehaviour
    {
        #region Runtime Variables
        [SerializeField]
        private List<ResistanceMapping> rawResistances
            = new List<ResistanceMapping>();
        private List<ElementModifier> resistances 
            = new List<ElementModifier>();
        #endregion

        #region MonoBehaviour Callbacks
        public void Start()
        {
            for (int i = 0; i < rawResistances.Count; i++)
            {
                UpdateResistances(i);
            }
        }
        #endregion

        #region Public APIs
        /// <summary>
        /// Adds or updates a resistance to an element by supplying a <see cref="ResistanceMapping"/>. 
        /// The struct definition can be found in <see cref="ElementalDamageCalculator"/>
        /// </summary>
        /// <param name="mapping">A struct containing the element, a function 
        /// specifying how damage from that element should be handled, 
        /// and hyperparameters that are used to vary that function. </param>
        public void AddResistance(ResistanceMapping mapping)
        {
            int index = UpdateRawResistances(mapping);
            UpdateResistances(index);
        }

        /// <summary>
        /// Resets the resistance of a given element. By definition in 
        /// <see cref="ElementalDamageCalculator"/>, this will reset 
        /// damage calculations to default. 
        /// </summary>
        /// <param name="element">Element of resistance you want to remove</param>
        public void RemoveResistance(Element element)
        {
            int position = -1;
            for (int i = 0; i < rawResistances.Count; i++)
            {
                if (element == rawResistances[i].element)
                {
                    position = i;
                    break;
                }
            }
            if (position == -1)
            {
                Debug.LogWarning("Resistance of type " + element.name +
                    "is not found in the resistance list.");
            }
            else
            {
                rawResistances.RemoveAt(position);
                resistances.RemoveAt(position);
            }
        }

        /// <summary>
        /// Returns the function that maps a given elemental damage to 
        /// final damage after deducting resistances, or null if no such 
        /// function is defined. 
        /// </summary>
        /// <param name="element">The element that you want to obtain</param>
        /// <returns></returns>
        public DamageTransform GetResistanceFunction(Element element)
        {
            foreach (ElementModifier map in resistances)
            {
                if (map.element == element)
                {
                    return map.damageTransform;
                }
            }
            return null;
        }
        #endregion

        #region Private Helpers
        private int UpdateRawResistances(ResistanceMapping mapping)
        {
            // I'm guessing this is an anti-pattern since we're using a list?
            for (int i = 0; i < rawResistances.Count; i++)
            {
                if (mapping.element == rawResistances[i].element)
                {
                    rawResistances[i] = mapping;
                    return i;
                }
            }
            rawResistances.Add(mapping);
            return (rawResistances.Count - 1);
        }

        private void UpdateResistances(int index)
        {
            ResistanceMapping map = rawResistances[index];

            DamageTransform newDamageTransform = Instantiate(map.damageTransform);
            newDamageTransform.SetupHyperparameters(map.hyperparameters);
            ElementModifier newMap = new ElementModifier()
            {
                element = map.element,
                damageTransform = newDamageTransform
            };

            if (index >= resistances.Count)
            {
                resistances.Add(newMap);
            }
            else
            {
                resistances[index] = newMap;
            }
        }
        #endregion
    }
}
