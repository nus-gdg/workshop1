using System;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// <see cref="IPoolableRecycler"/> allows registering to the
    /// <see cref="Poolable.Recycle"/> callback of a <see cref="Poolable"/> object.
    /// </summary>
    public interface IPoolableRecycler
    {
        /// <summary>
        /// Executes after observing the <see cref="Poolable.Recycle"/> callback of a <see cref="Poolable"/> object.
        /// </summary>
        /// <param name="itemId">The instance id of the <see cref="Poolable"/> object.</param>
        /// <seealso cref="UnityEngine.Object.GetInstanceID"/>
        void NotifyRecycle(int itemId);
    }

    /// <summary>
    /// <see cref="Poolable"/> is a template for components that should be managed by a <see cref="Pool{T}"/>.
    /// </summary>
    public abstract class Poolable : MonoBehaviour
    {
        /// <summary>
        /// The controller managing the usage of this object.
        /// </summary>
        public IPoolableRecycler recycler;

        /// <summary>
        /// Automatically disables the <see cref="GameObject"/> that this object is attached to.
        /// </summary>
        /// <remarks>
        /// Please do not override <see cref="Awake"/>.
        /// If you need to perform additional steps, override <see cref="OnAwake"/>.
        /// </remarks>
        public void Awake()
        {
            OnAwake();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Destroys the object by deactivating the <see cref="GameObject"/> that it is attached to,
        /// and returning it to its original <see cref="Pool{T}"/> for reuse.
        /// </summary>
        /// <remarks>
        /// Please do not override <see cref="Recycle"/>.
        /// If you need to perform additional steps, override <see cref="OnRecycle"/>.
        /// </remarks>
        public void Recycle()
        {
            OnRecycle();
            recycler?.NotifyRecycle(GetInstanceID());
            gameObject.SetActive(false);
        }
        
        /// <summary>
        /// <b>Warning</b>: <see cref="Destroy"/> has been disabled.
        /// Please use <see cref="Recycle"/> to destroy <see cref="Poolable"/> object.
        /// </summary>
        /// <remarks>
        /// Please do not override <see cref="Destroy"/>.
        /// </remarks>
        public void Destroy()
        {
            throw new InvalidOperationException("Please do not destroy poolable objects. Use Recycle() instead!");
        }

        /// <summary>
        /// Performs initialization during the <see cref="Awake"/> callback.
        /// </summary>
        /// <remarks>
        /// This ensures that <see cref="Poolable"/> objects are disabled when initialized.
        /// </remarks>
        protected virtual void OnAwake()
        { 
        }

        /// <summary>
        /// Performs initialization during the <see cref="Recycle"/> callback.
        /// </summary>
        /// <remarks>
        /// This ensures that <see cref="Poolable"/> objects are disabled when recycled.
        /// </remarks>
        protected virtual void OnRecycle()
        {
        }
    }
}
