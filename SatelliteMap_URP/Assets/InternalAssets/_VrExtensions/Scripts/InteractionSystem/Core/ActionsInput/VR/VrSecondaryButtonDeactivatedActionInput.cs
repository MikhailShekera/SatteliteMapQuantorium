namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public class VrSecondaryButtonDeactivatedActionInput  : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.SecondaryButtonState.JustDeactivated;
    }
}