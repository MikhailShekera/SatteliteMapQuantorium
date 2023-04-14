using UnityDevKit.InteractionSystem.Core.Args;

namespace UnityDevKit.InteractionSystem.Core
{
    public interface IInteractionEntity
    {
        void Focus(InteractionArgs args);
        void DeFocus(InteractionArgs args);
        void Interact(InteractionArgs args);
        void InteractHold(InteractionArgs args); // TODO -- mb extension
        void AfterInteract(InteractionArgs args);
    }
}