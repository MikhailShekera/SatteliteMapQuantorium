namespace UnityDevKit.InteractionSystem.Core.ActionsInput.Desktop
{
    public sealed class DesktopInteractActionInput : DesktopActionInput
    {
        public override bool Handle()
        {
            return InputManager.MovementControls.Movement.Click.WasPressedThisFrame();
        }
    }
}