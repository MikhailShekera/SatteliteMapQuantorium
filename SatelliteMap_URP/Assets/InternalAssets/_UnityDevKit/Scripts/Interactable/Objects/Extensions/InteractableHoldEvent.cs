using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public class InteractableHoldEvent : InteractableExtension
    {
        [SerializeField] private float holdTime = 1.5f;
        [SerializeField] private UnityEvent onHoldInteract;

        protected override void OnInteractAction(GameObject source)
        {
            base.OnInteractAction(source);
            Invoke(nameof(HoldInteract), holdTime);
        }

        protected override void OnAfterInteractAction(GameObject source)
        {
            base.OnAfterInteractAction(source);
            CancelInvoke();
        }

        private void HoldInteract()
        {
            onHoldInteract.Invoke();
        }
    }
}