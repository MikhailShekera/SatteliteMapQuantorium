namespace UnityDevKit.InteractionSystem.Core.ActionsInput.Desktop
{
    public sealed class DesktopInteractHoldActionInput : DesktopActionInput
    {
        public override bool Handle()
        {
            return InputManager.MovementControls.Movement.Click.IsPressed();
        }
    }
}