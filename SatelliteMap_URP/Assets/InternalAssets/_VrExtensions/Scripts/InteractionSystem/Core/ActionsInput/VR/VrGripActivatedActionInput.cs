namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public sealed class VrGripActivatedActionInput : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.GripButtonState.JustActivated;
    }
}