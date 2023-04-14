namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public class VrPrimaryButtonHoldActionInput  : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.PrimaryButtonState.Active;
    }
}