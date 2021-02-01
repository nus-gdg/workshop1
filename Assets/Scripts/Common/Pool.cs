using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common
{
    [Serializable]
    public abstract class Pool<T> : IPoolableRecycler where T : Poolable
    {
        /// <summary>
        /// A <see cref="Poolable"/> prefab.
        /// </summary>
        [SerializeField]
        private T prefab;
        /// <summary>
        /// The number of items to pool upon initialisation.
        /// </summary>
        [SerializeField]
        private int minAmount;
        /// <summary>
        /// Whether new items should be instantiated if the pool is emptied.
        /// </summary>
        [SerializeField]
        private bool shouldExpand;

        private Transform _parent;

        private Queue<T> _inactiveItems;
        private Dictionary<int, T> _activeItemsById;

        /// <summary>
        /// Prepares the settings and the required items for the pool.
        /// </summary>
        /// <param name="parent">Pool items will be nested under this <see cref="Transform"/>.</param>
        public void Initialize(Transform parent)
        {
            _parent = parent;

            _inactiveItems = new Queue<T>();
            _activeItemsById = new Dictionary<int, T>();

            for (int i = 0; i < minAmount; i++)
            {
                _inactiveItems.Enqueue(CreateItem());
            }
        }
        
        /// <summary>
        /// Returns true if the pool is able to offer a pool item, otherwise false.
        /// </summary>
        /// <param name="item">The next available pool item, or null if the pool is not flexible.</param>
        public bool GetItem(out T item)
        {
            if (_inactiveItems.Count <= 0)
            {
                // If the pool is emptied, check if more items should be created

                // Pool has a fixed size
                if (!shouldExpand)
                {
                    item = null;
                    return false;
                }
                // Pool is flexible
                item = CreateItem();
            }
            else
            {
                // Get the next available item in the pool
                item = _inactiveItems.Dequeue();
            }
            // Track the active item by their instance id
            _activeItemsById[item.GetInstanceID()] = item;
            return true;
        }

        public void NotifyRecycle(int itemId)
        {
            // Ignore instances that are not managed by the pool.
            if (!_activeItemsById.TryGetValue(itemId, out T item))
            {
                return;
            }
            // Stop tracking the item
            _activeItemsById.Remove(itemId);

            // Store the item for reuse
            _inactiveItems.Enqueue(item);
        }

        private T CreateItem()
        {
            // Prefab instance is guaranteed to have the required component
            // since it is serialized with the component.
            T item = Object.Instantiate(prefab, _parent).GetComponent<T>();
            
            // Allow this pool to manage the resources of the prefab instance
            item.recycler = this;

            return item;
        }
    }
}
