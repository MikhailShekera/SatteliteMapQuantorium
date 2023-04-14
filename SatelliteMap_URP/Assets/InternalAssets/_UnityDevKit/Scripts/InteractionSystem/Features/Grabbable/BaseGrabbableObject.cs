using System.Collections;
using MyBox;
using UnityDevKit.InteractionSystem.Interactable.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.InteractionSystem.Features.Grabbable
{
    public abstract class BaseGrabbableObject : InteractableExtension
    {
        [Separator("Grab/Throw process")]
        [SerializeField, PositiveValueOnly] private float grabbingTime = 0.2f;
        //[SerializeField, PositiveValueOnly] private float throwingTime = 0.05f;
        [SerializeField, PositiveValueOnly] private float throwVelocity = 2f;

        [Separator("Grab object")]
        [SerializeField] private Transform rootTransform;
        [SerializeField] protected Transform grabPoint;
        [SerializeField] private Rigidbody rb;

        [Separator("Actions")] 
        [SerializeField] private bool deactivateInteractableOnGrab = true;
        
        [Separator("Events")]
        [SerializeField] private UnityEvent onGrab;
        [SerializeField] private UnityEvent onThrow;
        
        private Transform _lastBaseHolder;
        private bool _rbIsKinematic;
        private bool _rbDetectCollisions;

        public Transform RootTransform => rootTransform; 
        
        protected const float EPSILON = 0.05f;
        protected const float ACCELERATION_MODIFIER = 1.05f;

        public IEnumerator Grab(GrabData grabData)
        {
            SaveLastInfo();
            rb.isKinematic = true;
            yield return MoveBeginProcess(grabData);
            OnGrabAction();
            onGrab.Invoke();
        }

        public IEnumerator Throw(GrabData grabData)
        {
            const float upperVelocityModifier = 0.15f;
            ResetState();
            rb.velocity = (grabData.GrabbableHolder.forward + Vector3.up * upperVelocityModifier) * throwVelocity;
            yield return null;
            OnThrowAction();
            onThrow.Invoke();
        }
        
        private void OnGrabAction()
        {
            if (deactivateInteractableOnGrab)
            {
                InteractionSource.Deactivate();
            }
        }
        
        private void OnThrowAction()
        {
            if (deactivateInteractableOnGrab)
            {
                InteractionSource.Activate();
            }
        }
        
        private void SaveLastInfo()
        {
            _lastBaseHolder = rootTransform.parent;
            _rbIsKinematic = rb.isKinematic;
            _rbDetectCollisions = rb.detectCollisions;
        }
        
        private void ResetState()
        {
            rootTransform.SetParent(_lastBaseHolder);
            rb.isKinematic = _rbIsKinematic;
            onThrow.Invoke();
        }

        private void RemoveCollisions()
        {
            rb.detectCollisions = false;
        }
        
        protected void ResetCollisions()
        {
            rb.detectCollisions = _rbDetectCollisions;
        }
        
        private IEnumerator MoveBeginProcess(GrabData grabData)
        {
            var lerpData = new GrabProcessData
            {
                GrabbingObjectTransform = rootTransform,
                HolderTransform = grabData.GrabbableHolder
            };

            lerpData.HolderTransform.localPosition = grabPoint.localPosition;
            lerpData.HolderTransform.localEulerAngles = grabPoint.localEulerAngles;

            lerpData.PositionTransitionSpeed =
                Vector3.Distance(
                    lerpData.GrabbingObjectTransform.position, 
                    lerpData.HolderTransform.position) / grabbingTime;
            lerpData.RotationTransitionSpeed =
                Vector3.Distance(
                    lerpData.GrabbingObjectTransform.eulerAngles, 
                    lerpData.HolderTransform.eulerAngles) / grabbingTime;

            RemoveCollisions();

            return GrabProcess(lerpData);
        }

        protected abstract IEnumerator GrabProcess(GrabProcessData lerpData);
    }
}