using UnityDevKit.Player.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityDevKit.InputSystem;

namespace SatelliteMap.Tests
{
    public class TestMapChange : MonoBehaviour, IInputControlsBinder
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerInput playerInput;

        private void Start()
        {
            AddBindings();
        }

        public void AddBindings()
        {
            playerController.InputManager.MovementControls.Movement.ChangeMapTest.performed += SwitchMap;
        }

        public void OnDisable()
        {
            playerController.InputManager.MovementControls.Movement.ChangeMapTest.performed -= SwitchMap;
        }

        private void SwitchMap(InputAction.CallbackContext callbackContext)
        {
            playerController.InputManager.ToggleActionMap(playerController.InputManager.MovementControls.Universal);
            Debug.Log(playerInput.currentActionMap);
        }
    }
}