using System.Collections.Generic;
using UnityDevKit.Events;
using UnityEngine;

namespace UnityDevKit.Storages
{
    public abstract class Storage<TData> : MonoBehaviour
    {
        public List<TData> Data { get; } = new List<TData>();

        public EventHolder<TData> OnDataSave { get; } = new EventHolder<TData>();
        public EventHolderBase OnDataClear { get; } = new EventHolderBase();

        public EventHolderBase OnDataRemove { get; } = new EventHolderBase();

        public virtual void Save(TData data)
        {
            Data.Add(data);
            OnDataSave.Invoke(data);
        }

        public void Clear()
        {
            Data.Clear();
            OnDataClear.Invoke();
        }

        public void Remove()
        {
            OnDataRemove.Invoke();
        }
    }
}