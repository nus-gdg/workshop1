using UnityEngine;
using System.Collections.Generic;

namespace Common
{
    /* Notes:
     * 1) Get a reference to this class (should be a singleton placed with 
     * a game manager)
     * 2) Instantiate objects using the Instantiate method in this class 
     * instead of using GameObject.Instantiate
     * 
     * In the object to be pooled:
     * 1) DO NOT DESTROY pooled objects (except for when exiting the 
     * application). Instead, disable them. 
     * 2) Call setup functions in OnEnable instead of Start. Also 
     * ensure that the prefab is disabled by default before assigning 
     * to the prefab
     * 
     * Protip: recall the lifecycle of Awake -> OnEnable -> Start
     * To Gabriel: if you need me to shift forward the initialisation, 
     * tell me and I'll make it initialise asynchronously on Awake
    */

    /// <summary>
    /// A struct containing data for PoolManager. \n
    /// objectToPool: a disabled prefab that you want to pool \n
    /// objectId: a name to reference the object when calling \n
    /// PoolManager.Instantiate \n
    /// amountToPool: number of objects to pool upon initialisation \n
    /// shouldExpand: whether new objects should be instantiated if pool \n
    /// is emptied
    /// </summary>
    [System.Serializable]
    public struct ObjectPoolItem
    {
        public GameObject objectToPool;
        public string objectId;
        public int amountToPool;
        public bool shouldExpand;
    }

    /// <summary>
    /// The PoolManager preloads commonly used objects to prevent \n
    /// lag when instantiating and destroying large number of objects.
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private ObjectPoolItem[] itemsToPool;
        private List<GameObject>[] pooledObjects;
        private Dictionary<string, int> stringHash;

        private void Awake()
        {
            // Don't iterate through a list to do string search, EVER. 
            stringHash = new Dictionary<string, int>();
            for (int i = 0; i < itemsToPool.Length; i++)
            {
                ObjectPoolItem itemData = itemsToPool[i];
                if (stringHash.ContainsKey(itemData.objectId))
                {
                    throw new System.ArgumentException(
                        "Pooled items contain duplicate object IDs: "
                        + itemData.objectId + " at index " + i.ToString()
                    );
                }
                stringHash[itemData.objectId] = i;
            }
        }

        private void Start()
        {
            //Initialises pooledObjects as an empty list of GameObjects.
            pooledObjects = new List<GameObject>[itemsToPool.Length];

            //Instantiates every object that's supposed to be pooled, disables them, and makes them a 
            //child of the gameObject containing this script to keep the hierarchy view neat.
            foreach (ObjectPoolItem item in itemsToPool)
            {
                int position = stringHash[item.objectId];
                pooledObjects[position] = new List<GameObject>();

                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool);

                    // Don't rely on this. It may not work
                    obj.SetActive(false);
                    obj.transform.SetParent(transform);

                    //After the object is instantiated, tells pooledObjects that this object is available
                    pooledObjects[position].Add(obj);
                }
            }
        }

        /// <summary>
        /// Retrieves a GameObject by a given object ID from the pool, 
        /// or returns null if there are no more GameObjects and the pool 
        /// cannot be expanded
        /// </summary>
        /// <param name="objectId">Name of object to instantiate</param>
        /// <returns>The pooled GameObject, or null if the pool is too small</returns>
        public GameObject Instantiate(string objectId)
        {
            int position = stringHash[objectId];
            return this.Instantiate(position);
        }

        /// <summary>
        /// Retrieves a GameObject by a given object ID from the pool, 
        /// or returns null if there are no more GameObjects and the pool 
        /// cannot be expanded
        /// </summary>
        /// <param name="position">Index of object to instantiate</param>
        /// <returns>The pooled GameObject, or null if the pool is too small</returns>
        public GameObject Instantiate(int position)
        {
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
                return obj;
            }

            // If there are no objects with the specified tag, 
            // or if the object pool for that object should not expand,
            // then nothing is returned.
            return null;
        }
    }
}
