using MyBox;
using System.Collections;
using UnityDevKit.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UnityDevKit.Player.InputSystem
{
    public class CrouchMovementExtension : RunModifierExtension, IInputControlsBinder
    {
        [Separator("Camera Component")]
        [SerializeField] private Camera faceCamera;

        [Separator("Crouch parameters")]
        [SerializeField, PositiveValueOnly] private float fullSquatTime = 1f;
        [SerializeField, PositiveValueOnly] private float fullSquatHeight = 1f;

        [Separator("Events")] 
        [SerializeField] private UnityEvent onSitDownStart;
        [SerializeField] private UnityEvent onStandUpStart;
        [SerializeField] private UnityEvent onSitDownEnd;
        [SerializeField] private UnityEvent onStandUpEnd;
        
        private bool _isSat;
        private float _maxHeight;
        private float _deltaHeight;
        private Transform _cameraTransform;
        private Vector3 _defaultCameraPosition;
        private Vector3 _targetCameraPosition;

        protected override void Awake()
        {
            base.Awake();

            _maxHeight = MainMovementComponent.Controller.height;
            _deltaHeight = _maxHeight - fullSquatHeight;
            _cameraTransform = faceCamera.transform;
            _defaultCameraPosition = _cameraTransform.localPosition;

            //Перенести в начало корутины "SittingDown", если необходимо менять высоту игрока на запущенной сцене 
            _targetCameraPosition = new Vector3(0,
                                                _cameraTransform.localPosition.y - _deltaHeight,
                                                0);
        }

        private void Start()
        {
            AddBindings();
        }

        public void AddBindings()
        {
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Crouch.performed += HandleCrouch;
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Crouch.canceled += HandleCrouch;
        }

        public void OnDisable()
        {
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Crouch.performed -= HandleCrouch;
            MainMovementComponent.PlayerController.InputManager.MovementControls.Movement.Crouch.canceled -= HandleCrouch;
        }

        #region Crouching
        public void HandleCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ControlSquat(true);
            }
            else if (context.canceled)
            {
                ControlSquat(false);
            }
        }

        private void ControlSquat(bool isCrouchingStarted)
        {
            StopAllCoroutines();
            _isSat = isCrouchingStarted;
            
            if (isCrouchingStarted)
            {
                HandleRunModifier(RunModifierType.Slow);
            }
            else
            {
                HandleDefaultRunModifier();
            }

            float coeff;

            if (isCrouchingStarted)
            {
                coeff = (MainMovementComponent.Controller.height - fullSquatHeight) / _deltaHeight;
            }
            else
            {
                coeff = (_maxHeight - MainMovementComponent.Controller.height) / _deltaHeight;
            }

            StartCoroutine(Squatting(coeff));
        }

        private IEnumerator Squatting(float coeff)
        {
            float elapsedTime = 0;
            float currentSquatTime = fullSquatTime * coeff;

            var startHeight = MainMovementComponent.Controller.height;
            var startLocalPosition = _cameraTransform.localPosition;
            
            if (_isSat)
            {
                onSitDownStart.Invoke();
            }
            else
            {
                onStandUpStart.Invoke();
            }

            while (elapsedTime < currentSquatTime)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / currentSquatTime;

                if (_isSat)
                {
                    MainMovementComponent.Controller.height = Mathf.Lerp(startHeight, fullSquatHeight, progress);
                    _cameraTransform.localPosition = Vector3.Lerp(startLocalPosition, _targetCameraPosition, progress);
                }
                else
                {
                    MainMovementComponent.Controller.height = Mathf.Lerp(startHeight, _maxHeight, progress);
                    _cameraTransform.localPosition = Vector3.Lerp(startLocalPosition, _defaultCameraPosition, progress);
                }
                MainMovementComponent.Controller.center = Vector3.down * (_maxHeight - MainMovementComponent.Controller.height) * 0.5f;

                yield return null;
            }

            if (_isSat)
            {
                onSitDownEnd.Invoke();
            }
            else
            {
                onStandUpEnd.Invoke();
            }
        }
        #endregion Crouching
    }
}