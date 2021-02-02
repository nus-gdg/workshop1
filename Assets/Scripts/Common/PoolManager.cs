using UnityEngine;
using System.Collections.Generic;

namespace Common
{
    /* Notes:
     * 1) Get a reference to this class (should be a singleton placed with 
     * a game manager)
     * 2) Instantiate objects using the InstantiatePooled method 
     * in this class instead of using GameObject.Instantiate
     * 
     * In the object to be pooled:
     * 1) DO NOT DESTROY pooled objects (except for when exiting the 
     * application). Instead, use the DestroyPooled method. 
     * If the prefab has children, ensure that your scripts call 
     * DestroyPooled on the prefab instead of its descendents. 
     * 2) Call setup functions in OnEnable instead of Start. Also 
     * ensure that the prefab is disabled by default before assigning 
     * to the prefab
     * 
     * Protip: recall the lifecycle of Awake -> OnEnable -> Start
     * To Gabriel: if you need me to shift forward the initialisation, 
     * tell me and I'll make it initialise asynchronously on Awake
    */

    /// <summary>
    /// <para>A struct containing data for PoolManager.</para>
    /// <para>objectToPool: a disabled prefab that you want to pool</para>
    /// <para>amountToPool: number of objects to pool upon initialisation</para>
    /// <para>shouldExpand: whether new objects should be instantiated if pool is emptied</para>
    /// </summary>
    [System.Serializable]
    public struct ObjectPoolItem
    {
        public GameObject objectToPool;
        public int amountToPool;
        public bool shouldExpand;
    }

    /// <summary>
    /// <para>The PoolManager preloads commonly used objects to prevent</para>
    /// lag when instantiating and destroying large number of objects.
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private ObjectPoolItem[] itemsToPool;

        private List<GameObject>[] pooledObjects;
        private Dictionary<int, int> idHash;
        // Note: not reliable, not updated if Destroy() is called
        private HashSet<int> instantiatedObjects; 

        private void Awake()
        {
            // Don't iterate through a list to do a search, EVER. 
            idHash = new Dictionary<int, int>();
            for (int i = 0; i < itemsToPool.Length; i++)
            {
                ObjectPoolItem itemData = itemsToPool[i];
                int id = itemData.objectToPool.GetInstanceID();
                if (idHash.ContainsKey(id))
                {
                    throw new System.ArgumentException(
                        "Duplicate prefab detected at index " + i.ToString()
                    );
                }
                idHash[id] = i;
            }
        }

        private void Start()
        {
            //Initialises pooledObjects as an empty list of GameObjects.
            pooledObjects = new List<GameObject>[itemsToPool.Length];
            instantiatedObjects = new HashSet<int>();

            //Instantiates every object that's supposed to be pooled, disables them, and makes them a 
            //child of the gameObject containing this script to keep the hierarchy view neat.
            foreach (ObjectPoolItem item in itemsToPool)
            {
                int id = item.objectToPool.GetInstanceID();
                int position = idHash[id];
                pooledObjects[position] = new List<GameObject>();

                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool);

                    // Don't rely on this. It may not work
                    obj.SetActive(false);
                    obj.transform.SetParent(transform);

                    //After the object is instantiated, tells pooledObjects that this object is available
                    pooledObjects[position].Add(obj);
                    instantiatedObjects.Add(obj.GetInstanceID());
                }
            }
        }

        /// <summary>
        /// <para>Retrieves a GameObject by a given prefab from the pool.</para>
        /// <para>If the prefab is not found in the pool, this method will 
        /// fallback to the default GameObject.Instantiate method 
        /// unless strict=true.</para>
        /// <para>The function may return null if the pool is empty and 
        /// cannot expand. If strict=true, the function throws an exception 
        /// if you attempt to retrieve a prefab not in the pool</para>
        /// </summary>
        /// <param name="prefab">Original prefab to instantiate</param>
        /// <param name="strict">Whether you want to fall back to 
        /// GameObject.Instantiate if object is not pooled</param>
        /// <returns>The pooled GameObject, or null if instantiation failed</returns>
        public GameObject InstantiatePooled(
            GameObject prefab,
            bool strict = false)
        {
            int id = prefab.GetInstanceID();
            if (idHash.ContainsKey(id))
            {
                int position = idHash[id];
                List<GameObject> selectedObjects = pooledObjects[position];

                for (int i = selectedObjects.Count - 1; i >= 0; i--)
                {
                    // This shouldn't happen if you disabled instead of 
                    // destroyed, but it's a failsafe.
                    if (selectedObjects[i] == null)
                    {
                        selectedObjects.RemoveAt(i);
                    }
                    else if (!selectedObjects[i].activeInHierarchy)
                    {
                        selectedObjects[i].SetActive(true);
                        return selectedObjects[i];
                    }
                }

                // If there are no inactive objects in the hierarchy, we 
                // expand the pool and return the new object provided that 
                // this is permitted.
                ObjectPoolItem itemData = itemsToPool[position];
                if (itemData.shouldExpand)
                {
                    GameObject obj = Instantiate(itemData.objectToPool);
                    obj.transform.SetParent(transform);
                    selectedObjects.Add(obj);
                    instantiatedObjects.Add(obj.GetInstanceID());
                    return obj;
                }

                // If there are no objects with the specified tag, 
                // or if the object pool for that object should not expand,
                // then nothing is returned.
                return null;
            }
            else
            {
                if (strict)
                {
                    throw new System.NullReferenceException(
                        "Object is not found in the pool: " +
                        prefab.name
                    );
                }
                else
                {
                    return Instantiate(prefab);
                }
            }
        }

        /// <summary>
        /// Destroys the supplied object using PoolManager's API if 
        /// the object is pooled, or destroys it normally if not pooled 
        /// unless strict=true, in which this function will throw an 
        /// exception instead. 
        /// </summary>
        /// <param name="obj">The object to destroy</param>
        /// <param name="strict">Whether you want to fall back to 
        /// GameObject.Instantiate if object is not pooled</param>
        public void DestroyPooled(Object obj, bool strict=false)
        {
            if (obj is GameObject gameObject)
            {
                int id = gameObject.GetInstanceID();
                if (instantiatedObjects.Contains(id))
                {
                    gameObject.SetActive(false);
                    gameObject.transform.SetParent(transform);
                    return;
                }
            }
            if (strict)
            {
                throw new System.NullReferenceException(
                    "Object is not found in the pool: " +
                    obj.name
                );
            }
            else
            {
                Destroy(obj);
            }
        }
    }
}
