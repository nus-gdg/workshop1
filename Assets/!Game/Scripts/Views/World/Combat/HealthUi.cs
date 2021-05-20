using UnityEngine;
using UnityEngine.UI;

namespace Project.Views.Combat
{
    public class HealthUi : MonoBehaviour
    {
        // To do (from Gabriel and Daniel): 
        // Make certain fields readonly? Add event system for death. 

        public int maxHealth = 100;
        public int currentHealth;
        public int dieValue = 0;
        public Slider healthSlider;
        public int fortesting = 0;

        // Start is called before the first frame update
        void Start()
        {
            /*
            ResetHealth();
            */
        }

        // Update is called once per frame
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
            currentHealth -= amt;
            if (currentHealth < dieValue) {
                Die();
            }

            healthSlider.value = (float) currentHealth/maxHealth;
        }

        void Die() {
            //what to do when dead
            Debug.Log("deadlol");
            // ResetHealth();
        }

        void ResetHealth() {
            currentHealth = maxHealth;
            healthSlider.value = 1;
        }
    }
}
