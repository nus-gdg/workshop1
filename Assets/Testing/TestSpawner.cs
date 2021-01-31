using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace danielnyan.Testing
{
    public class TestSpawner : MonoBehaviour
    {
        [SerializeField]
        private Common.PoolManager poolManager;
        private float angle = 0f;

        // Update is called once per frame
        void Update()
        {
            GameObject g = poolManager.Instantiate("Blue Bullet");
            if (g != null)
            {
                g.transform.position = transform.position;
                g.GetComponent<BulletScript>().velocity
                    = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * 20f;
            }

            // Check that the GameObject isn't null first before instantiating. 
            GameObject h = poolManager.Instantiate("Red Bullet");
            if (h != null)
            {
                h.transform.position = transform.position;
                h.GetComponent<BulletScript>().velocity
                    = new Vector3(Mathf.Cos(angle + Mathf.PI), Mathf.Sin(angle + Mathf.PI)) * 20f;
            }
            angle += 0.02f;
        }
    }
}
