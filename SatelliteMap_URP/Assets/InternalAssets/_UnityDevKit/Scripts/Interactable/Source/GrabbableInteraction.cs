using System;
using System.Collections;
using System.Linq;
using MyBox;
using UnityDevKit.Events;
using UnityDevKit.Interactable.Grabbable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDevKit.Interactables
{
    [Obsolete("Class is obsolete", false)]
    public class GrabbableInteraction : MonoBehaviour, IInteractionExtension
    {
        [SerializeField] [InitializationField] private float grabbingTime = 0.2f;
        [SerializeField] private GrabHolder[] grabHolders;

        public EventHolderBase onGrabStart = new EventHolderBase();
        public EventHolderBase onGrabEnd = new EventHolderBase();

        [Serializable]
        public sealed class GrabHolder
        {
            public Transform HoldTransform;
            public GrabbableObjectBase CurrentObject;
            public bool IsGrabed;
        }

        public GrabHolder[] GrabHolders => grabHolders;

        public void DropItem(InputAction.CallbackContext context)
        {
            Throw();
        }

        public bool Grab(GrabbableObjectBase grabbableObject)
        {
            var firstFreeHolder = grabHolders.FirstOrDefault(holder => holder.CurrentObject == null);
            if (firstFreeHolder == null) return false;
            firstFreeHolder.CurrentObject = grabbableObject;
            TakeObjectToHolder(firstFreeHolder);
            onGrabStart.Invoke();
            return true;
        }

        private void Throw()
        {
            var firstHolderWithObject = grabHolders.FirstOrDefault(holder => holder.CurrentObject != null && holder.IsGrabed);
            if (firstHolderWithObject == null) return;
            firstHolderWithObject.CurrentObject.ResetState();
            firstHolderWithObject.CurrentObject = null;
            firstHolderWithObject.IsGrabed = false;
            onGrabEnd.Invoke();
        }

        private void TakeObjectToHolder(GrabHolder holder)
        {
            StartCoroutine(MoveBeginProcess(holder));
        }

        private IEnumerator MoveBeginProcess(GrabHolder grabHolder)
        {
            GrabbableTempData lerpData = new GrabbableTempData();
            lerpData.grabbingObjectTransform = grabHolder.CurrentObject.RootTransform;
            lerpData.holderTransform = grabHolder.HoldTransform;

            lerpData.holderTransform.localPosition = grabHolder.CurrentObject.GrabPoint.localPosition;
            lerpData.holderTransform.localEulerAngles = grabHolder.CurrentObject.GrabPoint.localEulerAngles;

            lerpData.positionTransitionSpeed =
                Vector3.Distance(lerpData.grabbingObjectTransform.position, lerpData.holderTransform.position) / grabbingTime;
            lerpData.rotationTransitionSpeed =
                Vector3.Distance(lerpData.grabbingObjectTransform.eulerAngles, lerpData.holderTransform.eulerAngles) /
                grabbingTime;

            grabHolder.CurrentObject.RemoveCollisions();


            return grabHolder.CurrentObject.GrabProcess(grabHolder, lerpData);
        }
    }
}