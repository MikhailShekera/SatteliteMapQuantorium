using DG.Tweening;
using HurricaneVR.Framework.Core.Grabbers;
using MyBox;
using UnityDevKit.Interactable.Grabbable;
using UnityEngine;

namespace SatelliteMap.VR.RayPointer.Extensions
{
    public class VrRayGrabber : VrRayPointerExtension
    {
        [SerializeField, PositiveValueOnly] private float distanceMoveSpeed = 0.3f;
        
        private bool _isGrabbed;
        private Transform _currentGrabbable;
        private HVRHandGrabber _currentGrabber;
        private Transform _rootParent;

        private readonly Vector3 _grabAngle = new Vector3(90, 180, 90);
        
        protected override void OnPointerOut(LaserPointerEventArgs e)
        {
            base.OnPointerOut(e);
            var grabbable = e.Target.GetComponentInChildren<GrabbableUIObject>();
            if (grabbable)
            {
                Throw(grabbable, e.FromHand);
            }
        }
        protected override void OnPointerDown(LaserPointerEventArgs e)
        {
            base.OnPointerClick(e);
            var grabbable = e.Target.GetComponentInChildren<GrabbableUIObject>();
            if (grabbable)
            {
                Grab(grabbable, e.FromHand);
            }
        }
        
        protected override void OnPointerUp(LaserPointerEventArgs e)
        {
            base.OnPointerClick(e);
            var grabbable = e.Target.GetComponentInChildren<GrabbableUIObject>();
            if (grabbable)
            {
                Throw(grabbable, e.FromHand);
            }
        }

        protected override void OnPointerClickHold(bool isHolding)
        {
            base.OnPointerClickHold(isHolding);
            if (_isGrabbed && isHolding)
            {
                if (_currentGrabber.Controller.JoystickButtonState.Active)
                {
                    var deltaMoveDirection = _currentGrabber.Controller.GripButtonState.Active
                        ? -_currentGrabber.Palm.up
                        : _currentGrabber.Palm.up;
                    _currentGrabbable.position += deltaMoveDirection * distanceMoveSpeed * Time.deltaTime;
                }
            }
        }

        private void Grab(GrabbableUIObject grabbable, HVRHandGrabber grabber)
        {
            const float grabRotationDuration = 0.2f;
            if (_currentGrabber == null)
            {
                _rootParent = grabbable.RootTransform.parent;
            }
            grabbable.RootTransform.SetParent(grabber.Palm);

            var cachedLocalEulerAngles = grabbable.RootTransform.localEulerAngles;
            grabbable.RootTransform.localEulerAngles = _grabAngle;
            var targetEulerAngles = grabbable.RootTransform.eulerAngles;
            grabbable.RootTransform.localEulerAngles = cachedLocalEulerAngles;
            grabbable.RootTransform.DORotate(targetEulerAngles, grabRotationDuration);
            
            _currentGrabbable = grabbable.RootTransform;
            _currentGrabber = grabber;
            _isGrabbed = true;
        }

        private void Throw(GrabbableUIObject grabbable, HVRHandGrabber grabber)
        {
            if (_currentGrabber == grabber)
            {
                grabbable.RootTransform.SetParent(_rootParent);
                _currentGrabbable = null;
                _currentGrabber = null;
                _isGrabbed = false;
            }
        }
    }
}