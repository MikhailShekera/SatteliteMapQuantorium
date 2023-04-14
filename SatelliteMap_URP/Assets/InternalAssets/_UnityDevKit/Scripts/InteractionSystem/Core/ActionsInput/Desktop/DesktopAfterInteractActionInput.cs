namespace UnityDevKit.InteractionSystem.Core.ActionsInput.Desktop
{
    public sealed class DesktopAfterInteractActionInput : DesktopActionInput
    {
        public override bool Handle()
        {
            return InputManager.MovementControls.Movement.Click.WasReleasedThisFrame() ||
                   InputManager.MovementControls.Movement.Click.triggered &&
                   InputManager.MovementControls.Movement.Click.ReadValue<float>() == 0f;
        }
    }
}