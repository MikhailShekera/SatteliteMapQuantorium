using VrExtensions.InteractionSystem.Core.Args;
using InteractableExtension = UnityDevKit.InteractionSystem.Interactable.Extensions.InteractableExtension;

namespace VrExtensions.InteractionSystem.Interactable.Extensions
{
    public abstract class InteractableXrExtension : InteractableExtension
    {
        protected readonly VrInteractionArgsProcessor ArgsProcessor = new VrInteractionArgsProcessor();
    }
}