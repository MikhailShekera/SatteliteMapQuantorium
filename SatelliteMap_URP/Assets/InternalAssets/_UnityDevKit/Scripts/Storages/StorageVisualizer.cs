using MyBox;
using UnityEngine;

namespace UnityDevKit.Storages
{
    public abstract class StorageVisualizer<TStorage, TData> : BaseStorageVisualizer
    where TStorage : Storage<TData>
    {
        [Separator("Storage settings")]
        [SerializeField] private TStorage storage;

        

        private void Start()
        {
            
            
            Init(storage);
        }

        public override void Subscribe()
        {
            storage.OnDataSave.AddListener(OnStorageDataSaved);
            storage.OnDataClear.AddListener(OnStorageDataCleared);
            storage.OnDataRemove.AddListener(() => OnStorageDataRemoved(storage));
        }

        /// <summary>
        /// Initial setup
        /// </summary>
        /// <param name="storage">Storage to visualize</param>
        protected abstract void Init(TStorage storage);

        /// <summary>
        /// Callback for new data added to storage.
        /// Updates UI with new data 
        /// </summary>
        /// <param name="data">new data</param>
        protected abstract void OnStorageDataSaved(TData data);

        /// <summary>
        /// Callback for storage clear event.
        /// Resets UI
        /// </summary>
        protected abstract void OnStorageDataCleared();

        protected abstract void OnStorageDataRemoved(TStorage storage);
    }
}