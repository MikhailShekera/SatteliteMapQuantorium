using System.Collections;
using MyBox;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityDevKit.InteractionSystem.Features.Grabbable;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.InteractionSystem.Source.Extensions
{
    public class InteractionSourceGrabbing : InteractionSourceExtension
    {
        [SerializeField] private bool holdGrab;
        
        [SerializeField] private UnityEvent onGrabStart;
        [SerializeField] private UnityEvent onGrabEnd;
        [SerializeField] private UnityEvent onThrowStart;
        [SerializeField] private UnityEvent onThrowEnd;

        private Coroutine _grabbingCoroutine;
        private Coroutine _throwingCoroutine;
        private GrabbingProcess _grabState = GrabbingProcess.Empty;
        private BaseGrabbableObject _currentGrabbableObject;
        private InteractionArgs _currentInteractionArgs;

        private enum GrabbingProcess
        {
            Empty,
            Grabbing,
            Throwing,
            Grabbed
        }

        public UnityEvent OnGrabStart => onGrabStart;
        public UnityEvent OnGrabEnd => onGrabEnd;
        public UnityEvent OnThrowStar => onThrowStart;
        public UnityEvent OnThrowEnd => onThrowEnd;

        public BaseGrabbableObject CurrentGrabbableObject => _currentGrabbableObject;

        public InteractionArgs CurrentInteractionArgs => _currentInteractionArgs;

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            switch (_grabState)
            {
                case GrabbingProcess.Grabbed when !holdGrab:
                {
                    var grabData = BuildGrabData(args);
                    _throwingCoroutine = StartCoroutine(Throw(grabData));
                    break;
                }
                case GrabbingProcess.Empty when args.Target.TryGetComponent(out BaseGrabbableObject grabbable):
                {
                    var grabData = BuildGrabData(args);
                    _currentInteractionArgs = args;
                    _grabbingCoroutine = StartCoroutine(Grab(grabbable, grabData));
                    break;
                }
            }
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            if (!holdGrab) return;
            if (_grabState is GrabbingProcess.Grabbed )
            {
                var grabData = BuildGrabData(args);
                _throwingCoroutine = StartCoroutine(Throw(grabData));
            }
            else if (_grabState is GrabbingProcess.Grabbing)
            {
                var grabData = BuildGrabData(args);
                _throwingCoroutine = StartCoroutine(ThrowAfterGrab(grabData));
            }
        }

        private GrabData BuildGrabData(InteractionArgs args) => new GrabData
        {
            GrabbingSource = args.InteractionSource,
            GrabbableHolder = args.SourceHolder
        };

        private IEnumerator Grab(BaseGrabbableObject grabbable, GrabData grabData)
        {
            onGrabStart.Invoke();
            _grabState = GrabbingProcess.Grabbing;
            yield return grabbable.Grab(grabData);
            _currentGrabbableObject = grabbable;
            _grabState = GrabbingProcess.Grabbed;
            onGrabEnd.Invoke();
        }

        private IEnumerator Throw(GrabData grabData)
        {
            onThrowStart.Invoke();
            _grabState = GrabbingProcess.Throwing;
            yield return _currentGrabbableObject.Throw(grabData);
            _currentGrabbableObject = null;
            _currentInteractionArgs = null;
            _grabState = GrabbingProcess.Empty;
            onThrowEnd.Invoke();
        }

        private IEnumerator ThrowAfterGrab(GrabData grabData)
        {
            yield return _grabbingCoroutine;
            yield return Throw(grabData);
        }
    }
}