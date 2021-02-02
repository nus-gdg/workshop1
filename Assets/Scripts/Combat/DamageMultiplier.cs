using UnityEngine;

namespace Combat
{
    /// <summary>
    /// Multiplies incoming damage by an amount. See <see cref="DamageTransform"/>
    /// </summary>
    public class DamageMultiplier : DamageTransform
    {
        private float multiplier = 1f;
        
        /// <summary>
        /// Initialises the amount that incoming damage of a given 
        /// element is multiplied by. Default: 1
        /// </summary>
        /// <param name="parameters">An int[1] where the first parameter 
        /// is the multiplier.</param>
        public override void SetupHyperparameters(float[] parameters)
        {
            multiplier = parameters[0];
        }

        public override int DamageFunction(int damage)
        {
            return Mathf.RoundToInt(damage * multiplier);
        }
    }
}