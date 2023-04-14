using DG.Tweening;
using UnityDevKit.Utils;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Features.Rotation
{
    public sealed class RotationEngine
    {
        private readonly Transform _interactable;
        private readonly float _interactDistance;
        private readonly float _sens;

        private Vector3 _lastPosition;
        private Vector3 _lastAngle;
        private bool _offsetSet;
        private bool _inertiaSet;
        private bool _checkInteractDistance;
        private bool _useInertia;

        public RotationEngine(
            Transform interactable, 
            float interactDistance, 
            float sens, 
            bool checkInteractDistance=false,
            bool useInertia=false)
        {
            _interactable = interactable;
            _interactDistance = interactDistance;
            _sens = sens;
            _checkInteractDistance = checkInteractDistance;
            _useInertia = useInertia;
            _offsetSet = false;
            _inertiaSet = false;
        }

        public void Update(bool triggerActionState, Transform observableHand)
        {
            if (triggerActionState && IsCloseEnough(observableHand))
            {
                RotateByHand(observableHand);
                _inertiaSet = false;
            }
            else
            {
                _offsetSet = false;
                ApplyInertia();
                _inertiaSet = true;
            }
        }

        private bool IsCloseEnough(Transform hand)
        {
            return !_checkInteractDistance || Mathf.Abs(Vector3.Distance(hand.position, _interactable.transform.position)) <
                   _interactDistance;
        }

        private void RotateByHand(Transform hand)
        {
            if (!_offsetSet)
            {
                _lastPosition = hand.position;
                _offsetSet = true;
                return;
            }

            var currentPosition = hand.position;
            var interactablePosition = _interactable.position;
            float angle = FloatsUtils.CalculateThreePointSignedAngle(
                _lastPosition,
                interactablePosition,
                currentPosition);
            _lastAngle = new Vector3(0f, -angle * _sens, 0f);
            _interactable.Rotate(_lastAngle, Space.Self);

            _lastPosition = currentPosition;
        }
        
        private void ApplyInertia()
        {
            if (!_useInertia) return;
            if (_inertiaSet) return;
            
            var duration = 0.5f; // TODO
            var modifier = duration / Time.deltaTime; // TODO
            var inertiaAngle = _lastAngle * modifier;
            _interactable.DOLocalRotate(inertiaAngle, duration, RotateMode.LocalAxisAdd);
        }
    }
}