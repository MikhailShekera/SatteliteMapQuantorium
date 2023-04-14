using MyBox;
using System.Collections;
using UnityEngine;

namespace UnityDevKit.Player.Extensions
{
    [DisallowMultipleComponent]
    public class PlayerMovement : PlayerExtension
    {
        [SerializeField] private CharacterController controller;

        [SerializeField] [PositiveValueOnly] private float speed = 12f;
        [SerializeField] [PositiveValueOnly] private float maxRunModifier = 1.75f;
        [SerializeField] [PositiveValueOnly] private float crouchModifier = 0.6f;
        [SerializeField] [PositiveValueOnly] private float acceleratingDelta = 0.5f;
        [SerializeField] [PositiveValueOnly] private float slowDownDelta = 1f;

        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;


        [Separator("Crouch parameters")]
        [SerializeField][PositiveValueOnly] private float fullSquatTime = 1f;
        [SerializeField][PositiveValueOnly] private float fullSquatHeight = 1f;

        private Vector3 velocity;
        private float _maxHeight;
        private float _deltaHeight;
        private Transform _cameraTransform;
        private Vector3 _defaultCameraPosition;
        private Vector3 _targetCameraPosition;

        private bool isGrounded;
        private bool _isSat = false;

        public float RunModifier { get; private set; } = DefaultRunModifier;

        private const float DefaultRunModifier = 1f;

        protected override void Awake()
        {
            base.Awake();
            _maxHeight = controller.height;
            _deltaHeight = _maxHeight - fullSquatHeight;
            _cameraTransform = GetComponentInChildren<Camera>().transform;
            _defaultCameraPosition = _cameraTransform.localPosition;

            //Перенести в начало корутины "SittingDown", если необходимо менять высоту игрока на запущенной сцене 
            _targetCameraPosition = new Vector3(0,
                                                _cameraTransform.localPosition.y - _deltaHeight,
                                                0);
        }

        protected override void PlayerLoop()
        {
            base.PlayerLoop();

            HandleMovement();
            HandleGravity();
            HandleSquat();
        }

        private void HandleMovement()
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            HandleRunModifier();

            var move = TransformData.right * x + TransformData.forward * z;
            var normalizedMove = NormalizeMoveDelta(move) * RunModifier;

            controller.Move(normalizedMove * (speed * Time.deltaTime));
        }

        private void HandleRunModifier()
        {
            if (Input.GetKey(KeyCode.LeftShift) && RunModifier <= maxRunModifier && !_isSat)
            {
                RunModifier = Mathf.Min(RunModifier + acceleratingDelta * Time.deltaTime, maxRunModifier);
            }
            else if (RunModifier > crouchModifier && _isSat)
            {
                RunModifier = Mathf.Max(RunModifier - acceleratingDelta * Time.deltaTime, crouchModifier);
            }
            else if (RunModifier > DefaultRunModifier)
            {
                RunModifier = Mathf.Max(RunModifier - slowDownDelta * Time.deltaTime, DefaultRunModifier);
            }
            else if (RunModifier < DefaultRunModifier && !_isSat)
            {
                RunModifier = Mathf.Min(RunModifier + slowDownDelta * Time.deltaTime, DefaultRunModifier);
            }
        }

        #region Crouching
        private void HandleSquat()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                StopAllCoroutines();
                ControlSquat(true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                StopAllCoroutines();
                ControlSquat(false);
            }
        }

        private void ControlSquat(bool state)
        {
            _isSat = state;

            if(state)
                StartCoroutine(Squatting((controller.height - fullSquatHeight) / _deltaHeight));
            else
                StartCoroutine(Squatting((_maxHeight - controller.height) / _deltaHeight));
        }

        private IEnumerator Squatting(float coeff)
        {
            float elapsedTime = 0;
            float currentSquatTime = fullSquatTime * coeff;

            while (elapsedTime < currentSquatTime)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / currentSquatTime;

                if (_isSat)
                {
                    controller.height = Mathf.Lerp(controller.height, fullSquatHeight, progress);
                    _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _targetCameraPosition, progress);
                }
                else
                {
                    controller.height = Mathf.Lerp(controller.height, _maxHeight, progress);
                    _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _defaultCameraPosition, progress);
                }
                controller.center = Vector3.down * (_maxHeight - controller.height) * 0.5f;
             
                yield return null;
            }
        }
        #endregion Crouching

        private Vector3 NormalizeMoveDelta(Vector3 moveDelta)
        {
            var magnitude = moveDelta.magnitude;
            return magnitude > 1f ? moveDelta / magnitude : moveDelta;
        }

        private void HandleGravity()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                return;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}