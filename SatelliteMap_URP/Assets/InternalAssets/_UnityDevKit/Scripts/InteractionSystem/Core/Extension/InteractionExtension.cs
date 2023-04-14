using UnityDevKit.InteractionSystem.Core.Args;
using UnityDevKit.InteractionSystem.Core.Child;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Core.Extension
{
    public abstract class InteractionExtension<TInteractionEntity, TInteractionChild> : MonoBehaviour
        where TInteractionEntity : InteractionEntity
        where TInteractionChild : InteractionChild<TInteractionEntity>
    {
        private TInteractionEntity _interactionSource;
        protected TInteractionEntity InteractionSource => _interactionSource;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            InteractableEventsInit();
        }

        private void InteractableEventsInit()
        {
            if (!TryGetComponent(out _interactionSource))
            {
                if (TryGetComponent(out TInteractionChild interactableChild))
                {
                    _interactionSource = interactableChild.InteractionParent;
                }
                else
                {
                    Debug.LogError("Interaction extension has no reference to Interaction entity or Interaction entity child");
                }
            }
            
            _interactionSource.OnFocus.AddListener(OnFocusAction);
            _interactionSource.OnDeFocus.AddListener(OnDeFocusAction);
            _interactionSource.OnInteract.AddListener(OnInteractAction);
            _interactionSource.OnInteractHold.AddListener(OnInteractHoldAction);
            _interactionSource.OnAfterInteract.AddListener(OnAfterInteractAction);
            _interactionSource.OnActivateStateChanged.AddListener(OnActivateStateChangedAction);
        }

        protected virtual void OnFocusAction(InteractionArgs args)
        {
        }

        protected virtual void OnDeFocusAction(InteractionArgs args)
        {
        }

        protected virtual void OnInteractAction(InteractionArgs args)
        {
        }

        protected virtual void OnInteractHoldAction(InteractionArgs args)
        {
        }

        protected virtual void OnAfterInteractAction(InteractionArgs args)
        {
        }

        protected virtual void OnActivateStateChangedAction(bool isActivated)
        {
        }
    }
}