using System;
using MyBox;
using UnityDevKit.Player.Controllers;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Features.Rotation
{
    [Obsolete("Class is obsolete", false)]
    public class DesktopObjectRotation : MonoBehaviour
    {
        [Separator("VR")] 
        [SerializeField, InitializationField] private GameObject grabHolder;

        [Separator("Interactable")] 
        [SerializeField, InitializationField] private Transform interactable;

        [Separator("Rotation Parameters")] 
        [SerializeField, PositiveValueOnly, InitializationField] private float sens = 1;

        [SerializeField, PositiveValueOnly, InitializationField] private float interactDistance = 2f;

        [Separator("Player Controller")] [SerializeField]
        private PlayerController playerController;

        private RotationEngine _rotationEngine;

        private const string TARGET_RAG = "rotationObject";

        private void Start()
        {
            _rotationEngine = new RotationEngine(
                interactable,
                interactDistance,
                sens);
        }

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject.CompareTag(TARGET_RAG))
            {
                _rotationEngine.Update(
                    playerController.InputManager.MovementControls.Movement.Click.IsPressed(),
                    grabHolder.transform);
            }
        }
    }
}