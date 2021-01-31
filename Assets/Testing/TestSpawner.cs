using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace danielnyan.Testing
{
    public class TestSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject blueBullet;
        [SerializeField]
        private GameObject redBullet;
        [SerializeField]
        private GameObject yellowBullet;
        private float angle = 0f;
        private PoolManager poolManager;

        // Update is called once per frame
        void Update()
        {
            // Don't do this. Gabriel will implement the Game.Instance.Pool API
            poolManager = GetComponent<PoolManager>();

            GameObject g = poolManager.InstantiatePooled(blueBullet);
            if (g != null)
            {
                g.transform.position = transform.position;
                g.GetComponent<BulletScript>().velocity
                    = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * 20f;
            }

            // Check that the GameObject isn't null first before instantiating. 
            GameObject h = poolManager.InstantiatePooled(redBullet);
            if (h != null)
            {
                h.transform.position = transform.position;
                h.GetComponent<BulletScript>().velocity
                    = new Vector3(Mathf.Cos(angle + 2 * Mathf.PI / 3), Mathf.Sin(angle + 2 * Mathf.PI / 3)) * 20f;
            }

            GameObject i = poolManager.InstantiatePooled(yellowBullet);
            if (i != null)
            {
                i.transform.position = transform.position;
                i.GetComponent<BulletScript>().velocity
                    = new Vector3(Mathf.Cos(angle - 2 * Mathf.PI / 3), Mathf.Sin(angle - 2 * Mathf.PI / 3)) * 20f;
            }
            angle += 0.02f;
        }
    }
}
