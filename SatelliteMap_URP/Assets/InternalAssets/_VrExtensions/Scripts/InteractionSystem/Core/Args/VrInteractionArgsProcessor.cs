using HurricaneVR.Framework.Core.Grabbers;
using UnityDevKit.InteractionSystem.Core.Args;

namespace VrExtensions.InteractionSystem.Core.Args
{
    public class VrInteractionArgsProcessor : IInteractionArgsProcessor<VrInteractionArgs>
    {
        public VrInteractionArgs Process(InteractionArgs args)
            => new VrInteractionArgs
            {
                Args = args,
                HandGrabber = args.InteractionSource.GetComponent<HVRHandGrabber>()
            };
    }
}