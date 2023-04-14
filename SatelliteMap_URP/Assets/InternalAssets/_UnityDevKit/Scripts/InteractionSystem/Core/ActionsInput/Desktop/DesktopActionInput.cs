using UnityDevKit.InputSystem.Manager;
using UnityDevKit.Player.Controllers;

namespace UnityDevKit.InteractionSystem.Core.ActionsInput.Desktop
{
    public abstract class DesktopActionInput : InteractionActionInput
    {
        protected InputManager InputManager { get; private set; }
        
        private void Start()
        {
            InputManager = GetComponentInParent<PlayerController>().InputManager;
        }
    }
}