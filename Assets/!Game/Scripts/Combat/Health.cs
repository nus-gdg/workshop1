using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        // To do (from Gabriel and Daniel): 
        // Make certain fields readonly? Add event system for death. 

        public int maxHealth = 100;
        public int dieValue = 0;
        public Slider healthSlider;
        //need custom code to make it readonly?
        public int currentHealth;

        // Start is called before the first frame update
        void Start()
        {
            ResetHealth();
        }

        /*
        void Update()
        {
            if (fortesting > 1) {
                Damage(30);
                fortesting = 0;
            }
        }
        */

        //do damage
        public void Damage(int amt) {
            SetHealth(currentHealth - amt);
            if (currentHealth <= dieValue) {
                Die();
            }

            healthSlider.value = (float) currentHealth/maxHealth;
        }

        void ResetHealth() {
            SetHealth(maxHealth);
        }

        void SetHealth(int newHealth) {
            currentHealth = newHealth;
            healthSlider.value = (float) currentHealth/maxHealth;
        }

        public delegate void Death();
        public event Death OnDeath;
        void Die() {
            //what to do when dead
            Debug.Log("deadlol");
            if (OnDeath != null) {
                OnDeath();
                Debug.Log("calling ondeath");
            }
            // ResetHealth();
            //destroy the whole thing
            Destroy(transform.parent.gameObject);
        }
    }
}
