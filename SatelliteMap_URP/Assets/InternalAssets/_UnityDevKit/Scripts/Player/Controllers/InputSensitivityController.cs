using MyBox;
using UnityDevKit.Player.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDevKit.Player.Controllers
{
    public class InputSensitivityController : BindedPlayerExtension
    {
        [Separator("Mouse")]
        [SerializeField, PositiveValueOnly] private float mouseSensivity = 5;

        [Separator("Gamepad")]
        [SerializeField, PositiveValueOnly] private float gamepadSensivity = 100;

        private InputDevice _currentInputDevice;
        private InputAction _rotateCameraAction;

        public float MouseSensivity { get { return mouseSensivity; } set { mouseSensivity = value; } }

        protected override void Start()
        {
            base.Start();
        }
        public override void AddBindings()
        {
            _rotateCameraAction = PlayerController.InputManager.MovementControls.Universal.RotateCamera;

            _rotateCameraAction.performed += SetCurrentInputDevice;
        }

        public void SetCurrentInputDevice(InputAction.CallbackContext context)
        {
            if (context.control.device == Mouse.current)
            {
                _currentInputDevice = Mouse.current;
            }
            else if (context.control.device == Gamepad.current)
            {
                _currentInputDevice = Gamepad.current;
            }
            else
            {
                _currentInputDevice = null;
            }
        }

        public float GetSensivity()
        {
            switch(_currentInputDevice)
            {
                case Mouse:
                    return mouseSensivity;
                case Gamepad:
                    return gamepadSensivity;
                default:
                    Debug.Log(new System.ArgumentException("<color=red>Input Device Not Recognized</color>"));
                    return mouseSensivity;
             }
        }
    }
}
