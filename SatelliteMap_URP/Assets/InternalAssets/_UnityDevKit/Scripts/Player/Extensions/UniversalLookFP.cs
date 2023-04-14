using MyBox;
using UnityDevKit.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityDevKit.Utils.ScenesHandlers;
using UnityEngine.InputSystem;
using UnityDevKit.Player.Controllers;

namespace UnityDevKit.Player.Extensions
{
    [DisallowMultipleComponent]
    public class UniversalLookFP : BindedPlayerExtension
    {
        [Header("Main settings")]
        [SerializeField] private Transform playerBody;
        [SerializeField] private Camera cachedCamera;
        
        [Header("Crosshair settings")]
        [SerializeField] private Image crosshairImage;
        [SerializeField] private Canvas crosshairCanvas;
        [SerializeField] private bool crosshairOn;
        
        [Header("Extra settings")]
        [SerializeField] private bool useMouseMode;

        [SerializeField] [ConditionalField(nameof(useMouseMode))] private BoolTriggerEvent isFreeLookTrigger;
        [SerializeField] [ConditionalField(nameof(useMouseMode))] private int mouseModeToggleButton = 1;
        
        private float yRotation = 0f;
        //private Camera cachedCamera;

        private InputSensitivityController sensitiveController;

        private InputAction _cameraRotateAction;
        private InputAction _cursorModeSwitchAction;

        protected override void Awake()
        {
            base.Awake();
            //cachedCamera = GetComponentInChildren<Camera>();

            sensitiveController = GetComponentInChildren<InputSensitivityController>();
        }

        protected override void Start()
        {
            base.Start();

            TurnOffCursor();
            if (crosshairOn)
            {
                crosshairCanvas.gameObject.SetActive(true);
            }
        }

        public override void AddBindings()
        {
            _cameraRotateAction = PlayerController.InputManager.MovementControls.Universal.RotateCamera;
            _cursorModeSwitchAction = PlayerController.InputManager.MovementControls.Movement.ToggleCursorMode;

            _cursorModeSwitchAction.performed += ToggleCursorMode;
            _cameraRotateAction.performed += HandleMouseRotation;
        }

        private void OnDestroy()
        {
            _cursorModeSwitchAction.performed -= ToggleCursorMode;
            _cameraRotateAction.performed -= HandleMouseRotation;
        }

        protected override void PlayerLoop()
        {
            base.PlayerLoop();
        }

        private void HandleMouseRotation(InputAction.CallbackContext context)
        {
            float sensivity = sensitiveController.GetSensivity();

            if (isFreeLookTrigger.GetValue() && sensivity != 0 && Time.timeScale != 0)
            {
                var input = context.ReadValue<Vector2>();

                var mouseX = input.x * sensivity * Time.fixedDeltaTime;
                var mouseY = input.y * sensivity * Time.fixedDeltaTime;

                yRotation -= mouseY;
                yRotation = Mathf.Clamp(yRotation, -90f, 90f);

                cachedCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }

        public void ToggleCursorMode(InputAction.CallbackContext context)
        {
            if (useMouseMode)
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    TurnOnCursor();
                }
                else
                {
                    TurnOffCursor();
                }
            }
        }


        public void TurnOnCursor()
        {
            CursorHandler.TurnOnCursor();
            isFreeLookTrigger.SetValue(false);
        }

        public void TurnOffCursor()
        {
            isFreeLookTrigger.SetValue(true);
            CursorHandler.TurnOffCursor();
        }
    }
}