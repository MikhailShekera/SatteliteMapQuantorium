using UnityDevKit.InteractionSystem.Source.Extensions;
using VrExtensions.InteractionSystem.Core.Args;

namespace VrExtensions.InteractionSystem.Source.Extensions
{
    public abstract class InteractionSourceVrExtension : InteractionSourceExtension
    {
        protected readonly VrInteractionArgsProcessor ArgsProcessor = new VrInteractionArgsProcessor();
    }
}