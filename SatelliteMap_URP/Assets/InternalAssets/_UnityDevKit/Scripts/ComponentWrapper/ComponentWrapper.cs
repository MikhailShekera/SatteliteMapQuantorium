using UnityEngine;

namespace UnityDevKit.ComponentWrapper
{
    public class ComponentWrapper<TComponent> : MonoBehaviour
    {
        [SerializeField] private GameObject component;

        private TComponent _cachedComponent;

        public TComponent GetComponent()
        {
            _cachedComponent ??= component.GetComponent<TComponent>();
            return _cachedComponent;
        }
        
        private void Awake()
        {
            _cachedComponent ??= component.GetComponent<TComponent>();
        }
    }
}