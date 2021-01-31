using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace danielnyan.Testing
{
    public class BulletScript : MonoBehaviour
    {
        public Vector3 velocity;

        // Not needed, just an example for instantiation
        private Rigidbody rb;
        private bool setupDone = false;

        private void OnEnable()
        {
            StartCoroutine(Setup());
        }

        private IEnumerator Setup()
        {
            while (true)
            {
                rb = GetComponent<Rigidbody>();
                if (rb == null)
                {
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    setupDone = true;
                    break;
                }
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (!setupDone) return;
            transform.position += velocity * Time.fixedDeltaTime;
        }

        private void OnTriggerExit(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}
