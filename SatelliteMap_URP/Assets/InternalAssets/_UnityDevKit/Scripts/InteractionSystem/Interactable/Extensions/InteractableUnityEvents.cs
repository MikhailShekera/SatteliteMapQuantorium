using MyBox;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.InteractionSystem.Interactable.Extensions
{
    public class InteractableUnityEvents : InteractableExtension
    {
        [Separator("Events")] 
        [SerializeField] private UnityEvent<InteractionArgs> onFocusEvent;
        [SerializeField] private UnityEvent<InteractionArgs> onDeFocusEvent;
        [SerializeField] private UnityEvent<InteractionArgs> onInteractEvent;
        [SerializeField] private UnityEvent<InteractionArgs> onInteractHoldEvent;
        [SerializeField] private UnityEvent<InteractionArgs> onAfterInteractEvent;
        [SerializeField] private UnityEvent<bool> onActivateStateChangedEvent;
        
        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            onFocusEvent.Invoke(args);
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            onDeFocusEvent.Invoke(args);
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            onInteractEvent.Invoke(args);
        }

        protected override void OnInteractHoldAction(InteractionArgs args)
        {
            base.OnInteractHoldAction(args);
            onInteractHoldEvent.Invoke(args);
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            onAfterInteractEvent.Invoke(args);
        }
        
        protected override void OnActivateStateChangedAction(bool isActivated)
        {
            base.OnActivateStateChangedAction(isActivated);
            onActivateStateChangedEvent.Invoke(isActivated);
        }
    }
}