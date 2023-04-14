using System;
using UnityDevKit.Triggers;
using UnityEngine;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public class InteractableUiInput : InteractableExtension
    {
        [SerializeField] private BoolTriggerEvent lockTrigger;
        
        private InteractionBase _interactionBase;
        private bool _isLocked = true;
        
        private void Awake()
        {
            _interactionBase = FindObjectOfType<InteractionBase>();
        }

        protected override void Init()
        {
            base.Init();
            lockTrigger.SubscribeToValueChanged(value => _isLocked = value);
            lockTrigger.SubscribeToTrueValueSet(() => Interactable.DeFocus());
        }

        private void OnMouseEnter()
        {
            if (_isLocked) return;
            Interactable.Focus(_interactionBase.gameObject);
        }

        private void OnMouseExit()
        {
            if (_isLocked) return;
            Interactable.DeFocus();
        }

        private void OnMouseDown()
        {
            if (_isLocked) return;
            Interactable.Interact();
        }
    }
}