using System;
using UnityDevKit.Interactables.Children;
using UnityEngine;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public abstract class InteractableExtension : MonoBehaviour
    {
        private InteractableBase _interactable;
        protected InteractableBase Interactable => _interactable;
        
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
            if (!TryGetComponent(out _interactable))
            {
                if (TryGetComponent(out InteractableChild interactableChild))
                {
                    _interactable = interactableChild.Interactable;
                }
                else
                {
                    Debug.LogError("InteractableExtension has no reference to InteractableBase or InteractableChild");
                }
            }


            _interactable.OnFocus.AddListener(OnFocusAction);
            _interactable.OnDeFocus.AddListener(OnDeFocusAction);
            _interactable.OnInteract.AddListener(OnInteractAction);
            _interactable.OnAfterInteract.AddListener(OnAfterInteractAction);

            _interactable.OnActiveStateChange.AddListener(OnActiveStateChangedAction);
            _interactable.OnStopStateChange.AddListener(OnStopStateChangeAction);
        }

        protected virtual void OnFocusAction(GameObject source)
        {
        }

        protected virtual void OnDeFocusAction(GameObject source)
        {
        }

        protected virtual void OnInteractAction(GameObject source)
        {
        }

        protected virtual void OnAfterInteractAction(GameObject source)
        {
        }

        protected virtual void OnActiveStateChangedAction(bool isActive)
        {
        }

        protected virtual void OnStopStateChangeAction(bool isStopped)
        {
        }
    }
}