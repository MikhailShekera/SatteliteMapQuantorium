using UnityEngine.InputSystem;

namespace UnityDevKit.Player.InputSystem
{
    public static class InputActionsPressedStatus
    {
        public static bool IsPressed(InputAction inputAction)
        {
            return inputAction.ReadValue<float>() > 0f;
        }
    }
}