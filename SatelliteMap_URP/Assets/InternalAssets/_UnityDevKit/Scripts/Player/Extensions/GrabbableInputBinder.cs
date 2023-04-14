using UnityDevKit.Interactables;
using UnityDevKit.Interactables.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDevKit.Player.Extensions
{
    [RequireComponent(typeof(GrabbableInteraction))]
    public class GrabbableInputBinder : BindedPlayerExtension
    {
        private GrabbableInteraction grabbableInteractionComponent;
        private GrabbedPushPullExtension grabbedPushPullExtension;

        private InputAction _dropGrabbableObject;
        private InputAction _pushPullGrabbedObject;

        protected override void Awake()
        {
            base.Awake();
            
            grabbableInteractionComponent = GetComponent<GrabbableInteraction>();
            if(TryGetComponent<GrabbedPushPullExtension>(out var component))
            {
                grabbedPushPullExtension = component;
            }
        }

        protected override void Start()
        {
            base.Start();

            grabbableInteractionComponent.onGrabStart.AddListener(SetGrabbableInputMap);
            grabbableInteractionComponent.onGrabEnd.AddListener(SetMovementMap);
        }

        public override void AddBindings()
        {
            _dropGrabbableObject = PlayerController.InputManager.MovementControls.Grabbing.Drop;
            _pushPullGrabbedObject = PlayerController.InputManager.MovementControls.Grabbing.PushPullGrabbedObject;

            _dropGrabbableObject.performed += grabbableInteractionComponent.DropItem;
            if (grabbedPushPullExtension != null)
            {
                _pushPullGrabbedObject.performed += grabbedPushPullExtension.PushPullGrabbedObject;
            }
        }

        private void SetGrabbableInputMap()
        {
            PlayerController.InputManager.ToggleActionMap(PlayerController.InputManager.MovementControls.Grabbing);
        }

        private void SetMovementMap()
        {
            PlayerController.InputManager.ToggleActionMap(PlayerController.InputManager.MovementControls.Movement);
        }
    }
}
