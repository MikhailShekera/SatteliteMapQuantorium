namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public sealed class VrGripHoldActionInput : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.GripButtonState.Active;
    }
}