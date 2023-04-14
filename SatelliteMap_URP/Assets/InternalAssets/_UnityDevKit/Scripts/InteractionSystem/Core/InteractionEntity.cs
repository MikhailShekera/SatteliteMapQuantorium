using MyBox;
using UnityDevKit.Events;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Core
{
    public abstract class InteractionEntity : MonoBehaviour, IInteractionEntity
    {
        [Separator("Controlled actions")]
        [SerializeField] protected bool useFocus = true;
        [SerializeField] protected bool useDeFocus = true;
        [SerializeField] protected bool useInteract = true;
        [SerializeField] protected bool useInteractHold = true;
        [SerializeField] protected bool useAfterInteract = true;
        
        public readonly EventHolder<InteractionArgs> OnFocus = new EventHolder<InteractionArgs>();
        public readonly EventHolder<InteractionArgs> OnDeFocus = new EventHolder<InteractionArgs>();
        public readonly EventHolder<InteractionArgs> OnInteract = new EventHolder<InteractionArgs>();
        public readonly EventHolder<InteractionArgs> OnInteractHold = new EventHolder<InteractionArgs>();
        public readonly EventHolder<InteractionArgs> OnAfterInteract = new EventHolder<InteractionArgs>();
        public readonly EventHolder<bool> OnActivateStateChanged = new EventHolder<bool>();

        protected bool IsActivated { get; private set; } = true;
        
        public void Focus(InteractionArgs args)
        {
            if (!useFocus) return;
            ActionPreprocess(OnFocus, args);
        }

        public void DeFocus(InteractionArgs args)
        {
            if (!useDeFocus) return;
            ActionPreprocess(OnDeFocus, args);
        }

        public void Interact(InteractionArgs args)
        {
            if (!useInteract) return;
            ActionPreprocess(OnInteract, args);
        }

        public void InteractHold(InteractionArgs args)
        {
            if (!useInteractHold) return;
            ActionPreprocess(OnInteractHold, args);
        }

        public void AfterInteract(InteractionArgs args)
        {
            if (!useAfterInteract) return;
            ActionPreprocess(OnAfterInteract, args);
        }

        public virtual void Activate()
        {
            IsActivated = true;
            ActivateActionPreprocess();
        }
        
        public virtual void Deactivate()
        {
            IsActivated = false;
            ActivateActionPreprocess();
        }
        
        public void Toggle()
        {
            IsActivated = !IsActivated;
            ActivateActionPreprocess();
        }

        private void ActionPreprocess(EventHolder<InteractionArgs> eventHolder, InteractionArgs args)
        {
            if (!IsActivated || args.Target == null) return;
            eventHolder.Invoke(args);
        }

        private void ActivateActionPreprocess()
        {
            OnActivateStateChanged.Invoke(IsActivated);
        }
    }
}