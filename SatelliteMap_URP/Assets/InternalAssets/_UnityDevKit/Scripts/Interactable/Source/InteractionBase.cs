using System;
using UnityEngine;

namespace UnityDevKit.Interactables
{
    [Obsolete("Class is obsolete", false)]
    public class InteractionBase : MonoBehaviour
    {
        private InteractableBase currentInteractable;
        private bool isFocused;

        private void Awake()
        {
            LoadExtensions();
        }

        private void Start()
        {
            isFocused = false;
        }

        // ----- FOCUS -----
        public void FocusObject(InteractableBase interactable)
        {
            if (currentInteractable != null && currentInteractable != interactable)
            {
                currentInteractable.DeFocus();
            }

            isFocused = true;
            currentInteractable = interactable;
            currentInteractable.Focus(gameObject);
        }

        public void LoseFocus()
        {
            if (currentInteractable != null)
            {
                currentInteractable.DeFocus();
                currentInteractable = null;
            }

            isFocused = false;
        }

        // ----- INTERACTING -----
        public void Interact()
        {
            if (currentInteractable == null) return;
            currentInteractable.Interact();
        }

        public void AfterInteract()
        {
            if (currentInteractable == null) return;
            currentInteractable.AfterInteract();
        }

        public void ForceInteractable(InteractableBase interactableBase)
        {
            currentInteractable = interactableBase;
        }

        public IInteractionExtension[] Extensions { get; private set; }

        private void LoadExtensions()
        {
            Extensions = GetComponentsInChildren<IInteractionExtension>();
        }
    }
}