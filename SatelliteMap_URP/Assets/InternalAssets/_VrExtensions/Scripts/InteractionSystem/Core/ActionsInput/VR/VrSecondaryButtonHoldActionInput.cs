namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public class VrSecondaryButtonHoldActionInput  : VrActionInput
    {
        public override bool Handle() => handGrabber.Controller.SecondaryButtonState.Active;
    }
}