using MyBox;
using UnityDevKit.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityDevKit.Utils.ScenesHandlers;

namespace UnityDevKit.Player.Extensions
{
    [DisallowMultipleComponent]
    public class MouseLookFp : PlayerExtension
    {
        [Header("Main settings")]
        [SerializeField] private float mouseSens = 100f;
        [SerializeField] private Transform playerBody;
        
        [Header("Crosshair settings")]
        [SerializeField] private Image crosshairImage;
        [SerializeField] private Canvas crosshairCanvas;
        [SerializeField] private bool crosshairOn;
        
        [Header("Extra settings")]
        [SerializeField] private bool useMouseMode;

        [SerializeField] [ConditionalField(nameof(useMouseMode))] private BoolTriggerEvent isFreeLookTrigger;
        [SerializeField] [ConditionalField(nameof(useMouseMode))] private int mouseModeToggleButton = 1;
        
        private float yRotation = 0f;
        private Camera cachedCamera;

        protected override void Awake()
        {
            base.Awake();
            cachedCamera = GetComponentInChildren<Camera>();
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

        protected override void PlayerLoop()
        {
            base.PlayerLoop();
            if (isFreeLookTrigger.GetValue())
            {
                var mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
                var mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

                yRotation -= mouseY;
                yRotation = Mathf.Clamp(yRotation, -90f, 90f);

                cachedCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }

            if (useMouseMode && Input.GetMouseButtonUp(mouseModeToggleButton))
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