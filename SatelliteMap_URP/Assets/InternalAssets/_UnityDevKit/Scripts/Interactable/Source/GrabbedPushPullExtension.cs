using System;
using MyBox;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public class GrabbedPushPullExtension : MonoBehaviour
    {
        [Separator("Move Modifier")]
        [SerializeField, PositiveValueOnly] private float pushPullModifier = 1;

        private GrabbableInteraction mainScript;

        private void Awake()
        {
            if (!TryGetComponent(out mainScript))
            {
                throw new System.NullReferenceException("Missing GrabbableInteraction component");
            }
        }

        public void PushPullGrabbedObject(InputAction.CallbackContext context)
        {
            var firstFreeHolder = mainScript.GrabHolders.FirstOrDefault(holder => holder.CurrentObject.gameObject);
            var currentGrabbedObject = firstFreeHolder.CurrentObject;

            float maximumGrabDistance = currentGrabbedObject.MaximumGrabDistance;

            var grabbingObjectTransform = currentGrabbedObject.RootTransform;
            var holderTransform = firstFreeHolder.HoldTransform;

            if (currentGrabbedObject != null)
            {
                float delta = context.ReadValue<float>();
                Vector3 position = grabbingObjectTransform.localPosition;

                if (delta > 0)
                {

                    Vector3 targetPosition = position += Vector3.forward * pushPullModifier;

                    if (Vector3.Distance(holderTransform.localPosition, targetPosition) < maximumGrabDistance)
                    {
                        grabbingObjectTransform.localPosition += Vector3.forward * pushPullModifier;
                    }
                }
                else if (delta < 0)
                {
                    Vector3 targetPosition = position -= Vector3.forward * pushPullModifier;

                    if (Vector3.Distance(holderTransform.localPosition, targetPosition) < Vector3.Distance(holderTransform.localPosition, grabbingObjectTransform.localPosition))
                    {
                        grabbingObjectTransform.localPosition -= Vector3.forward * pushPullModifier;
                    }

                }
            }
        }
    }
}
