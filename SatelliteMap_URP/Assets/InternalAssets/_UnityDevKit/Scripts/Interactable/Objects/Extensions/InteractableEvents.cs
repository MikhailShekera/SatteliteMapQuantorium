using System;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public class InteractableEvents : InteractableExtension
    {
        [Separator("Events")] 
        [SerializeField] private UnityEvent onInteractEvent;
        [SerializeField] private UnityEvent onAfterInteractEvent;
        [SerializeField] private UnityEvent onFocusEvent;
        [SerializeField] private UnityEvent onDeFocusEvent;
        [SerializeField] private UnityEvent<bool> onActivateStateChangeEvent;
        [SerializeField] private UnityEvent onActivateEvent;
        [SerializeField] private UnityEvent onDeactivateEvent;

        public UnityEvent OnInteractEvent => onInteractEvent;
        public UnityEvent OnAfterInteractEvent=> onAfterInteractEvent;
        public UnityEvent OnFocusEvent=> onFocusEvent;
        public UnityEvent OnDeFocusEvent=> onDeFocusEvent;
        public UnityEvent<bool> OnActivateStateChangeEvent => onActivateStateChangeEvent;
        public UnityEvent OnActivateEvent=> onActivateEvent;
        public UnityEvent OnDeactivateEvent=> onDeactivateEvent;
        
        protected override void OnFocusAction(GameObject source)
        {
            base.OnFocusAction(source);
            onFocusEvent.Invoke();
        }

        protected override void OnDeFocusAction(GameObject source)
        {
            base.OnDeFocusAction(source);
            onDeFocusEvent.Invoke();
        }

        protected override void OnInteractAction(GameObject source)
        {
            base.OnInteractAction(source);
            onInteractEvent.Invoke();
        }

        protected override void OnAfterInteractAction(GameObject source)
        {
            base.OnAfterInteractAction(source);
            onAfterInteractEvent.Invoke();
        }

        protected override void OnActiveStateChangedAction(bool isActive)
        {
            base.OnActiveStateChangedAction(isActive);
            onActivateStateChangeEvent.Invoke(isActive);
            if (isActive)
            {
                onActivateEvent.Invoke();
            }
            else
            {
                onDeactivateEvent.Invoke();
            }
        }
    }
}