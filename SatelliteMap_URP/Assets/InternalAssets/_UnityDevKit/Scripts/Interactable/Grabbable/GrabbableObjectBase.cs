using System;
using MyBox;
using System.Collections;
using UnityDevKit.Interactables;
using UnityDevKit.Interactables.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UnityDevKit.Interactable.Grabbable
{
    [DisallowMultipleComponent] [Obsolete("Class is obsolete", false)]
    public abstract class GrabbableObjectBase : InteractableExtension
    {
        [Separator("Grab Distance")]
        [SerializeField, PositiveValueOnly] private float maximumGrabDistace = 1;

        [Separator("Body Settings")]
        [SerializeField] private Transform rootTransform;
        [SerializeField] private Transform grabPoint;
        [SerializeField] private Rigidbody rb;

        [Separator("Events")]
        [SerializeField] private UnityEvent onGrab;
        [SerializeField] private UnityEvent onThrow;
        
        private Transform _lastBaseHolder;
        private bool _rbIsKinematic;
        private bool _rbDetectCollisions;

        private PlayerInput playerInput;

        protected const float EPSILON = 0.05f;
        protected const float ACCELERATION_MODIFIER = 1.05f;

        public Transform RootTransform => rootTransform;

        public Transform GrabPoint => grabPoint;

        public float MaximumGrabDistance => maximumGrabDistace;

        public UnityEvent OnGrab => onGrab;
        public UnityEvent OnThrow => onThrow;

        public abstract IEnumerator GrabProcess(GrabbableInteraction.GrabHolder grabHolder, GrabbableTempData lerpData);
       
        protected override void OnInteractAction(GameObject source)
        {
            base.OnInteractAction(source);
            var grabInteraction = source.GetComponentInChildren<GrabbableInteraction>();
            if (grabInteraction == null)
            {
                Debug.LogError("Player tried to grab object without GrabbableInteraction");
                return;
            }
            SaveLastInfo();
            var hasGrabbed = grabInteraction.Grab(this);

            if (hasGrabbed)
            {
                rb.isKinematic = true;
                
                Interactable.Activate(false);
                onGrab.Invoke();
            }
        }

        private void SaveLastInfo()
        {
            _lastBaseHolder = rootTransform.parent;
            _rbIsKinematic = rb.isKinematic;
            _rbDetectCollisions = rb.detectCollisions;
        }
        
        public void ResetState()
        {
            rootTransform.SetParent(_lastBaseHolder);
            rb.isKinematic = _rbIsKinematic;

            Interactable.Activate(true);
            onThrow.Invoke();
        }

        public void RemoveCollisions()
        {
            rb.detectCollisions = false;
        }
        
        public void ResetCollisions()
        {
            rb.detectCollisions = _rbDetectCollisions;
        }
    }
}