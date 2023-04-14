namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public class VrPrimaryButtonActivatedActionInput  : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.PrimaryButtonState.JustActivated;
    }
}