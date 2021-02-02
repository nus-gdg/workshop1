using UnityEngine;

namespace Combat
{
    /// <summary>
    /// An abstract class that governs how raw damage is mapped to final damage.
    /// </summary>
    public abstract class DamageTransform : ScriptableObject
    {
        /// <summary>
        /// Defines how class parameters grabbed by DamageFunction are set up
        /// </summary>
        /// <param name="parameters">An array of ints passed during runtime</param>
        public abstract void SetupHyperparameters(float[] parameters);

        /// <summary>
        /// Takes in raw damage after buffs but before defence and outputs final damage.
        /// </summary>
        /// <param name="damage">Incoming damage</param>
        /// <returns></returns>
        public abstract int DamageFunction(int damage);
    }
}
