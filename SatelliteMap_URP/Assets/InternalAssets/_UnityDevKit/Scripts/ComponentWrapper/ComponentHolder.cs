using System;
using MyBox;
using UnityEngine;

namespace UnityDevKit.ComponentWrapper
{
    [Serializable]
    public class ComponentHolder<TComponent>
    {
        [InitializationField] [SerializeField]
        private bool useComponentWrapper;
        
        [InitializationField] [ConditionalField(nameof(useComponentWrapper))] [SerializeField]
        private ComponentWrapper<TComponent> componentWrapper;
        
        [InitializationField] [ConditionalField(nameof(useComponentWrapper), true)] [SerializeField]
        private TComponent component;
        
        public TComponent WrappedComponent 
            => useComponentWrapper 
            ? componentWrapper.GetComponent() 
            : component;
    }
}