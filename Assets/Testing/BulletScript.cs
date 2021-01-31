using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace danielnyan.Testing
{
    public class BulletScript : MonoBehaviour
    {
        public Vector3 velocity;

        // Not needed, just an example for instantiation
        private Rigidbody rb;
        // Don't do this. Reference the global instance instead
        private PoolManager pool;
        private bool setupDone = false;

        private void OnEnable()
        {
            StartCoroutine(Setup());
        }

        private IEnumerator Setup()
        {
            int currentPhase = 0;
            while (true)
            {
                if (currentPhase == 0)
                {
                    rb = GetComponent<Rigidbody>();
                    if (rb == null)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                    else
                    {
                        currentPhase += 1;
                    }
                }
                if (currentPhase == 1)
                {
                    // Don't do this. Use the appropriate API instead
                    pool = GameObject.Find("Bullet Spawner and Pooler").GetComponent<PoolManager>();
                    if (pool == null)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                    else
                    {
                        currentPhase += 1;
                    }
                }
                if (currentPhase == 2)
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
            pool.DestroyPooled(gameObject);
        }
    }
}
