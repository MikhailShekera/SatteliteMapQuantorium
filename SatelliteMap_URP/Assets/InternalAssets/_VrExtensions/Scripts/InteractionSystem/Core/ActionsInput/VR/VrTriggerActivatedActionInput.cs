namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public sealed class VrTriggerActivatedActionInput : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.TriggerButtonState.JustActivated;
    }
}