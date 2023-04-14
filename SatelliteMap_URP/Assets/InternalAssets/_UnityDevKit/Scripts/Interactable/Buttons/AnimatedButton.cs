using System.Collections;
using MyBox;
using UnityDevKit.ComponentWrapper;
using UnityDevKit.Optimization;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Interactables.Buttons
{
    public class AnimatedButton : CachedMonoBehaviour
    {
        [Separator("Settings")] 
        [SerializeField] [InitializationField]
        private ComponentHolder<Transform> transformHolder;
        
        [SerializeField] [InitializationField] private bool startPressedState = false;

        [SerializeField] [InitializationField] [PositiveValueOnly]
        private float transitionTime = 0.25f;

        [InitializationField] [SerializeField] [PositiveValueOnly] [Range(0.001f, 99.9f)]
        private float epsilonPercentage = 0.05f;

        [Separator("Transition delta")] 
        [SerializeField] [InitializationField] private TransitionMoveVector moveDelta;
        [SerializeField] [InitializationField] private TransitionRotationVector rotationDelta = new TransitionRotationVector
        {
            moveDelta = Vector3.zero
        };

        [Separator("Events")] 
        [SerializeField] private UnityEvent onBtnToggle;
        [SerializeField] private UnityEvent onBtnDown;
        [SerializeField] private UnityEvent onBtnUp;

        public UnityEvent OnButtonToggle => onBtnToggle;
        public UnityEvent OnButtonDown => onBtnDown;
        public UnityEvent OnButtonUp => onBtnUp;
        
        private Transform _movablePart;
        private bool _isPressed;
        
        private void Start()
        {
            Init();
        }

        public void Toggle()
        {
            StopWorkingCoroutines();

            StartCoroutine(ToggleProcess());
        }

        public void ToggleWithReturn()
        {
            StopWorkingCoroutines();
            _isPressed = startPressedState;

            StartCoroutine(FullMoveProcess());
        }

        public void SetDown()
        {
            if (_isPressed)
            {
                return;
            }

            Toggle();
        }

        public void SetUp()
        {
            if (!_isPressed)
            {
                return;
            }

            Toggle();
        }

        public void SetTransformHolder(ComponentHolder<Transform> newTransformHolder)
        {
            transformHolder = newTransformHolder;
#if !UNITY_EDITOR
            Init();
#endif
        }

        public void SetMoveDelta(TransitionMoveVector newMoveDelta)
        {
            moveDelta = newMoveDelta;
#if !UNITY_EDITOR
            Init();
#endif
        }

        private void Init()
        {
            _movablePart = transformHolder.WrappedComponent;
            _isPressed = startPressedState;

            moveDelta.Setup(_movablePart, _movablePart.localPosition, transitionTime, epsilonPercentage);
            rotationDelta.Setup(_movablePart, _movablePart.localEulerAngles, transitionTime, epsilonPercentage);

            if (_isPressed)
            {
                _movablePart.localPosition = moveDelta.movedPosition;
                _movablePart.localEulerAngles = rotationDelta.movedPosition;
            }
            
            onBtnDown.AddListener(onBtnToggle.Invoke);
            OnButtonUp.AddListener(onBtnToggle.Invoke);
        }

        private IEnumerator ToggleProcess()
        {
            Vector3 targetPosition;
            Vector3 targetRotation;
            if (_isPressed)
            {
                _movablePart.localPosition = moveDelta.movedPosition; 
                targetPosition = moveDelta.startPosition;

                _movablePart.localEulerAngles = rotationDelta.movedPosition;
                targetRotation = rotationDelta.startPosition;
            }
            else
            {
                _movablePart.localPosition = moveDelta.startPosition;
                targetPosition = moveDelta.movedPosition;
                
                _movablePart.localEulerAngles = rotationDelta.startPosition;
                targetRotation = rotationDelta.movedPosition;
            }
            
            moveDelta.currentCoroutine = StartCoroutine(moveDelta.MoveProcess(targetPosition));
            rotationDelta.currentCoroutine = StartCoroutine(rotationDelta.MoveProcess(targetRotation));

            _isPressed = !_isPressed;

            yield return moveDelta.currentCoroutine;
            yield return rotationDelta.currentCoroutine;
            
            if (_isPressed)
            {
                onBtnDown.Invoke();
            }
            else
            {
                onBtnUp.Invoke();
            }
        }

        private IEnumerator FullMoveProcess()
        {
            yield return ToggleProcess();
            yield return ToggleProcess();
        }

        private void StopWorkingCoroutines()
        {
            StopWorkingCoroutine(moveDelta);
            StopWorkingCoroutine(rotationDelta);
        }

        private void StopWorkingCoroutine(TransitionVector transitionVector)
        {
            if (transitionVector.currentCoroutine != null)
            {
                StopCoroutine(transitionVector.currentCoroutine);
            }
        }
    }
}