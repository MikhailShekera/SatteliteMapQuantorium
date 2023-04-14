namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public sealed class VrTriggerHoldActionInput : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.TriggerButtonState.Active;
    }
}