namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public sealed class VrTriggerDeactivatedActionInput : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.TriggerButtonState.JustDeactivated;
    }
}