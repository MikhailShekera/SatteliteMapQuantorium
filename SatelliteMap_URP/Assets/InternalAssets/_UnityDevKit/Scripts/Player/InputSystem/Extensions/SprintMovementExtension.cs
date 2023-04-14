using UnityDevKit.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDevKit.Player.InputSystem
{
    public class SprintMovementExtension : RunModifierExtension, IInputControlsBinder
    {
        private void Start()
        {
            AddBindings();
        }

        public void AddBindings()
        {
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Sprint.performed += Accelerate;
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Sprint.started += Accelerate;
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Sprint.canceled += Slow;
        }

        public void OnDisable()
        {
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Sprint.performed -= Accelerate;
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Sprint.started -= Accelerate;
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Sprint.canceled -= Slow;
        }

        private void Accelerate(InputAction.CallbackContext _)
        {
            Accelerate();
        }
        
        private void Accelerate()
        {
            var wasHandled = HandleRunModifier(RunModifierType.Accelerate);
            if (!wasHandled)
            {
                Debug.Log("Accelerate restart");
                const float updatePeriod = 0.4f;
                Invoke(nameof(Accelerate), updatePeriod);
            }
        }

        private void Slow(InputAction.CallbackContext _)
        {
            Slow();
        }
        
        private void Slow()
        {
            CancelInvoke();
            HandleDefaultRunModifier();
        }
    }
}