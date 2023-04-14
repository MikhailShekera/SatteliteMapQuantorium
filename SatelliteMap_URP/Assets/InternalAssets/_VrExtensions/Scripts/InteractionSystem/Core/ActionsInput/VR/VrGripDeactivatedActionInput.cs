namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public sealed class VrGripDeactivatedActionInput : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.GripButtonState.JustDeactivated;
    }
}