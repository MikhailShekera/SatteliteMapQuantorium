using MyBox;
using UnityDevKit.InputSystem.Manager;
using UnityDevKit.InteractionSystem.Source.Extensions;
using UnityDevKit.Player.Controllers;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Features.Grabbable
{
    [RequireComponent(typeof(InteractionSourceGrabbing))]
    public sealed class GrabbablePushPullExtension : MonoBehaviour
    {
        [Separator("Move Modifier")] 
        [SerializeField, PositiveValueOnly] private float maximumGrabDistance = 10;
        [SerializeField, PositiveValueOnly] private float pushPullModifier = 1;

        private InteractionSourceGrabbing _sourceGrabbing;
        private InputManager _inputManager;

        private void Start()
        {
            _sourceGrabbing = GetComponent<InteractionSourceGrabbing>();
            _inputManager = GetComponentInParent<PlayerController>().InputManager;
            _inputManager.MovementControls.Grabbing.Enable();
        }

        private void Update()
        {
            PushPullGrabbedObject();
        }

        private void PushPullGrabbedObject()
        {
            var currentGrabbableObject = _sourceGrabbing.CurrentGrabbableObject;
            if (currentGrabbableObject == null) return;

            var grabbingObjectTransform = currentGrabbableObject.RootTransform;
            var holderTransform = _sourceGrabbing.CurrentInteractionArgs.SourceHolder;
            
            float delta = _inputManager.MovementControls.Grabbing.PushPullGrabbedObject.ReadValue<float>();
            Vector3 position = grabbingObjectTransform.localPosition;

            switch (delta)
            {
                case > 0:
                {
                    Vector3 targetPosition = position + Vector3.forward * pushPullModifier;

                    if (Vector3.Distance(holderTransform.localPosition, targetPosition) < maximumGrabDistance)
                    {
                        grabbingObjectTransform.localPosition += Vector3.forward * pushPullModifier;
                    }

                    break;
                }
                case < 0:
                {
                    Vector3 targetPosition = position - Vector3.forward * pushPullModifier;

                    if (Vector3.Distance(holderTransform.localPosition, targetPosition) <
                        Vector3.Distance(holderTransform.localPosition, grabbingObjectTransform.localPosition))
                    {
                        grabbingObjectTransform.localPosition -= Vector3.forward * pushPullModifier;
                    }

                    break;
                }
            }
        }
    }
}