namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public class VrSecondaryButtonActivatedActionInput  : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.SecondaryButtonState.JustActivated;
    }
}