using UnityEngine;

namespace UnityDevKit.Storages
{
    public class StorageVisualiserSetter : MonoBehaviour
    {
        [SerializeField] private BaseStorageVisualizer storageVisualizer;

        private void Start()
        {
            storageVisualizer.Subscribe();
        }
    }
}