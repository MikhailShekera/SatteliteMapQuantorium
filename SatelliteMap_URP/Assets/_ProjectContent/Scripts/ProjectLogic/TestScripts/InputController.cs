using UnityDevKit.Player.InputSystem;
using UnityEngine;

namespace SatelliteMap.Tests
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private CrouchMovementExtension crouch;

        private PlayerMovementControls playerControls;

        private void Awake()
        {
            playerControls = new PlayerMovementControls();
        }

        private void OnEnable()
        {
            Debug.Log("Connected");
            playerControls.Enable();
            playerControls.Movement.Crouch.performed += crouch.HandleCrouch;
            playerControls.Movement.Crouch.canceled += crouch.HandleCrouch;
        }
    }
}