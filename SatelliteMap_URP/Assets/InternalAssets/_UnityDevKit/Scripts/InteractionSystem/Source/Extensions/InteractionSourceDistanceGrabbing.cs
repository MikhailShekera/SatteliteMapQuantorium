using System.Collections;
using DG.Tweening;
using MyBox;
using UnityDevKit.InteractionSystem.Core.ActionsInput;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityDevKit.InteractionSystem.Features.Grabbable;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.InteractionSystem.Source.Extensions
{
    public class InteractionSourceDistanceGrabbing : InteractionSourceExtension
    {
        [Separator("Grab")]
        [SerializeField] private bool smoothRotateOnGrab = true;

        [Separator("Push Pull")] 
        [SerializeField] private bool allowPushPull = true;
        
        [SerializeField, PositiveValueOnly, ConditionalField(nameof(allowPushPull))] 
        private float distanceMoveSpeed = 3f;

        [SerializeField, ConditionalField(nameof(allowPushPull))]
        private PushPullMode pushPullMode = PushPullMode.Buttons;
        
        [SerializeField, ConditionalField(nameof(allowPushPull))] 
        private InteractionAxisActionInput pullPushAxisInput;
        
        [SerializeField, ConditionalField(nameof(allowPushPull))] 
        private InteractionActionInput pullInput; 
        
        [SerializeField, ConditionalField(nameof(allowPushPull))] 
        private InteractionActionInput pushInput;

        [Separator("Events")] 
        [SerializeField] private UnityEvent onGrab;
        [SerializeField] private UnityEvent onThrow;
        
        private bool _isGrabbed;
        private Transform _currentGrabbable;
        private Transform _currentGrabber;
        private Transform _rootParent;
        
        private readonly Vector3 _grabAngle = new Vector3(90, 180, 90);

        private enum PushPullMode
        {
            Axis, Buttons
        }

        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            if (args == null || args.Target == null) return;
            var grabbable = args.Target.GetComponentInChildren<GrabbableUiObject>();
            if (grabbable && grabbable.RootTransform == _currentGrabbable)
            {
                StopAllCoroutines();
            }
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            var grabbable = args.Target.GetComponentInChildren<GrabbableUiObject>();
            if (grabbable)
            {
                Throw(grabbable, args.SourceHolder);
            }
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            var grabbable = args.Target.GetComponentInChildren<GrabbableUiObject>();
            if (grabbable)
            {
                Grab(grabbable, args.SourceHolder);
            }
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            var grabbable = args.Target.GetComponentInChildren<GrabbableUiObject>();
            if (grabbable)
            {
                Throw(grabbable, args.SourceHolder);
            }
        }

        protected override void OnInteractHoldAction(InteractionArgs args)
        {
            base.OnInteractHoldAction(args);
            if (!_isGrabbed) return;
            if (!allowPushPull) return;
            var pushPullDirection = CalculatePushPullModifier();
            if (pushPullDirection == 0) return;
            _currentGrabbable.position += pushPullDirection * _currentGrabber.up * distanceMoveSpeed * Time.deltaTime;
        }

        private float CalculatePushPullModifier()
        {
            if (pushPullMode == PushPullMode.Axis)
            {
                return -pullPushAxisInput.GetAxis().y;
            }
            var push = pushInput.Handle() ? 1 : 0;
            var pull = pullInput.Handle() ? -1 : 0;
            return pull + push;
        }

        private void Grab(GrabbableUiObject grabbable, Transform grabber)
        {
            const float grabRotationDuration = 0.2f;
            if (_currentGrabber == null)
            {
                _rootParent = grabbable.RootTransform.parent;
            }
            grabbable.RootTransform.SetParent(grabber);

            if (smoothRotateOnGrab)
            {
                var cachedLocalEulerAngles = grabbable.RootTransform.localEulerAngles;
                grabbable.RootTransform.localEulerAngles = _grabAngle;
                var targetEulerAngles = grabbable.RootTransform.eulerAngles;
                grabbable.RootTransform.localEulerAngles = cachedLocalEulerAngles;
                grabbable.RootTransform.DORotate(targetEulerAngles, grabRotationDuration);  
            }

            _currentGrabbable = grabbable.RootTransform;
            _currentGrabber = grabber;
            _isGrabbed = true;
            onGrab.Invoke();
        }

        private void Throw(GrabbableUiObject grabbable, Transform grabber)
        {
            if (_currentGrabber == grabber)
            {
                StartCoroutine(ThrowProcess(grabbable));
            }
        }

        private IEnumerator ThrowProcess(GrabbableUiObject grabbable)
        {
            const float safeDelay = 0.1f; 
            yield return new WaitForSeconds(safeDelay);
            grabbable.RootTransform.SetParent(_rootParent);
            _currentGrabbable = null;
            _currentGrabber = null;
            _isGrabbed = false;
            onThrow.Invoke();
        }
    }
}