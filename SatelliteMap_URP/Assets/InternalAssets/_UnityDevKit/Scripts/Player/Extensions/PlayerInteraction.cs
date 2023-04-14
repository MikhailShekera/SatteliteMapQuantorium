using System.Collections.Generic;
using MyBox;
using UnityDevKit.Interactables;
using UnityDevKit.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDevKit.Player.Extensions
{
    public class PlayerInteraction : BindedPlayerExtension
    {
        [Separator("Interaction settings")] 
        [SerializeField] private float distance = 3f;

        [SerializeField] private LayerMask layerMask;

        [SerializeField] private bool useTagFilter = false;

        [SerializeField] [ConditionalField(nameof(useTagFilter))]
        private List<string> ignoredTags;

        [SerializeField] private BoolTriggerEvent isFreeLookTrigger;
        
        private Camera mainCamera;
        private Transform mainCameraTransform;

        private InteractionBase interaction;
        private InputAction _clickAction;

        private const int INTERACTING_FRAME_DELAY = 10;

        protected override void Awake()
        {
            base.Awake();
            interaction = GetComponent<InteractionBase>();
        }

        protected override void Start()
        {
            _clickAction = PlayerController.InputManager.MovementControls.Movement.Click;
            base.Start();
            mainCamera = Camera.main;
            mainCameraTransform = mainCamera.transform;
        }

        public override void AddBindings()
        {
            _clickAction.performed += GetInteraction;
            _clickAction.canceled += GetInteraction;
        }

        public void UnbindClick()
        {
            _clickAction.performed -= GetInteraction;
            _clickAction.canceled -= GetInteraction;
        }

        protected override void PlayerLoop()
        {
            base.PlayerLoop();
            if (!isFreeLookTrigger.GetValue()) return;
            if (Time.frameCount % INTERACTING_FRAME_DELAY == 0)
            {
                Searching();
            }
        }

        public void GetInteraction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                interaction.Interact();
            }
            else if (context.canceled)
            {
                interaction.AfterInteract();
            }
        }

        private void Searching()
        {
            var ray = new Ray(mainCameraTransform.position, mainCameraTransform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.magenta);

            if (Physics.Raycast(ray, out var hitInfo, distance, layerMask))
            {
                if (!useTagFilter || !ignoredTags.Contains(hitInfo.collider.tag))
                {
                    var hitObject = hitInfo.collider.GetComponent<InteractableBase>();
                    if (hitObject != null)
                    {
                        interaction.FocusObject(hitObject);
                        // TODO;
                        return;
                    }
                }
            }

            interaction.LoseFocus();
        }
    }
}