using MyBox;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityDevKit.InteractionSystem.Features.Rotation;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Interactable.Extensions
{
    public sealed class InteractableObjectRotation : InteractableExtension
    {
        [Separator("Grab settings")]
        [SerializeField, InitializationField] private Transform rotatableObject;

        [Separator("Rotation Parameters")]
        [SerializeField, PositiveValueOnly, InitializationField] private float sens = 5;
        [SerializeField, PositiveValueOnly, InitializationField] private float interactDistance = 3f;
        [SerializeField] private string targetTag = "rotationObject";

        private RotationEngine _rotationEngine;

        private void Awake()
        {
            _rotationEngine = new RotationEngine(
                rotatableObject,
                interactDistance,
                sens);
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            UpdateEngine(false, args);
        }

        protected override void OnInteractHoldAction(InteractionArgs args)
        {
            base.OnInteractHoldAction(args);
            UpdateEngine(true, args);
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            UpdateEngine(false, args);
        }

        private void UpdateEngine(bool actionState, InteractionArgs args)
        {
            if (!args.Target.CompareTag(targetTag)) return;
            _rotationEngine.Update(actionState, args.SourceHolder);
        }
    }
}