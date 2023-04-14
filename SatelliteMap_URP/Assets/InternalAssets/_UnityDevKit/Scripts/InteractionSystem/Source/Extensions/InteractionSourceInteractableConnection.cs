using UnityDevKit.InteractionSystem.Core.Args;
using UnityDevKit.InteractionSystem.Interactable;

namespace UnityDevKit.InteractionSystem.Source.Extensions
{
    public class InteractionSourceInteractableConnection : InteractionSourceExtension
    {
        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            if (args.Target.TryGetComponent(out BaseInteractable interactable))
            {
                interactable.Focus(args);
            }
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            if (args.Target.TryGetComponent(out BaseInteractable interactable))
            {
                interactable.DeFocus(args);
            }
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            if (args.Target.TryGetComponent(out BaseInteractable interactable))
            {
                interactable.Interact(args);
            }
        }

        protected override void OnInteractHoldAction(InteractionArgs args)
        {
            base.OnInteractHoldAction(args);
            if (args.Target.TryGetComponent(out BaseInteractable interactable))
            {
                interactable.InteractHold(args);
            }
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            if (args.Target.TryGetComponent(out BaseInteractable interactable))
            {
                interactable.AfterInteract(args);
            }
        }
    }
}