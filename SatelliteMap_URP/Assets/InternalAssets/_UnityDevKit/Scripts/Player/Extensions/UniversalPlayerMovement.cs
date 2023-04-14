using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDevKit.Player.Extensions
{
    [DisallowMultipleComponent]
    public class UniversalPlayerMovement : PlayerExtension
    {
        [SerializeField] private CharacterController controller;

        [Separator("Privary Movement Settings")]
        [SerializeField] [PositiveValueOnly] private float speed = 12f;
        [SerializeField] [PositiveValueOnly] private float smoothMovementDelta = 0.3f;

        [Separator("Gravity And Ground")]
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;

        private Vector3 _velocity;

        private Vector2 _currentInputVector;
        private Vector2 _currentInputVelocity;

        private bool isGrounded;

        public float RunModifier { get; set; } = DefaultRunModifier;
        public CharacterController Controller => controller;
        public float DefaultMovementModifier => DefaultRunModifier;

        private const float DefaultRunModifier = 1f;

        private InputAction _moveAction;

        protected override void Start()
        {
            base.Start();
            _moveAction = PlayerController.InputManager.MovementControls.Universal.Move;
        }

        protected override void PlayerLoop()
        {
            base.PlayerLoop();

            HandleMovement();
            HandleGravity();
        }

        public void HandleMovement()
        {
            var input = _moveAction.ReadValue<Vector2>();
            _currentInputVector = Vector2.SmoothDamp(_currentInputVector, input, ref _currentInputVelocity, smoothMovementDelta);

            var move = TransformData.right * _currentInputVector.x + TransformData.forward * _currentInputVector.y;
            var normalizedMove = NormalizeMoveDelta(move) * RunModifier;

            controller.Move(normalizedMove * (speed * Time.deltaTime));
        }

        private Vector3 NormalizeMoveDelta(Vector3 moveDelta)
        {
            var magnitude = moveDelta.magnitude;
            return magnitude > 1f ? moveDelta / magnitude : moveDelta;
        }

        private void HandleGravity()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
                return;
            }

            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);
        }
    }
}