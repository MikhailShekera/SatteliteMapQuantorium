namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public class VrPrimaryButtonDeactivatedActionInput  : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.PrimaryButtonState.JustDeactivated;
    }
}