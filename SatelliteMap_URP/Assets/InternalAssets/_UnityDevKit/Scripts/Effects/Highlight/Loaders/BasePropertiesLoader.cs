using UnityEngine;

namespace UnityDevKit.Effects.Highlight.Loaders
{
    public abstract class BasePropertiesLoader<TProperties> : MonoBehaviour, IPropertiesLoader<TProperties>
    {
        public abstract TProperties GetProperties();
    }
}