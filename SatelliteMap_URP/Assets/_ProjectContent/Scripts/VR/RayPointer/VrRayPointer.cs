using HurricaneVR.Framework.Core.Grabbers;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

namespace SatelliteMap.VR.RayPointer
{
    public class VrRayPointer : MonoBehaviour
    {
        // [Separator("VR settings")] 

        [Separator("Laser settings")]
        [SerializeField] private Transform origin;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float maxRayDistance = 100f;

        [Separator("Pointer settings")] 
        [SerializeField, Range(1, 120)] private int frameCountFilter = 1;

        [Separator("Events")] 
        [SerializeField] private UnityEvent<bool, float> onRaycast;
        [SerializeField] private UnityEvent onPointerActivated;
        [SerializeField] private UnityEvent onPointerDeactivated;
        [SerializeField] private UnityEvent<LaserPointerEventArgs> onPointerIn;
        [SerializeField] private UnityEvent<LaserPointerEventArgs> onPointerClick;
        [SerializeField] private UnityEvent<bool> onPointerClickHold;
        [SerializeField] private UnityEvent<LaserPointerEventArgs> onPointerDown;
        [SerializeField] private UnityEvent<LaserPointerEventArgs> onPointerUp;
        [SerializeField] private UnityEvent<LaserPointerEventArgs> onPointerOut;

        public Transform LaserOrigin => origin;
        
        public UnityEvent<bool, float> OnRaycast => onRaycast;
        public UnityEvent OnPointerActivatedEvent => onPointerActivated;
        public UnityEvent OnPointerDeactivatedEvent => onPointerDeactivated;
        public UnityEvent<LaserPointerEventArgs> OnPointerInEvent => onPointerIn;
        public UnityEvent<LaserPointerEventArgs> OnPointerClickEvent => onPointerClick;
        public UnityEvent<bool> OnPointerClickHoldEvent => onPointerClickHold;
        public UnityEvent<LaserPointerEventArgs> OnPointerOutEvent => onPointerOut;
        public UnityEvent<LaserPointerEventArgs> OnPointerDownEvent => onPointerDown;
        public UnityEvent<LaserPointerEventArgs> OnPointerUpEvent => onPointerUp;
        
        private HVRHandGrabber _hand;
        private bool _isWorking;
        private Transform _previousContact;
        private float _currentDistance;
        
        private void Awake()
        {
            if (!TryGetComponent(out _hand))
            {
                Debug.LogError("Laser has no hand source", this);
            }
        }

        private void Update()
        {
            if (!_isWorking || Time.frameCount % frameCountFilter != 0) return;

            var hasHit = Physics.Raycast(
                origin.position, 
                origin.forward, 
                out var hit, 
                maxRayDistance,
                layerMask);
            _currentDistance = hasHit ? hit.distance : maxRayDistance;
            
            OnRaycastBase(hasHit, _currentDistance);
            ProcessPointer(hasHit, hit);
        }

        #region API

        public void Show()
        {
            _isWorking = true;
            onPointerActivated.Invoke();
        }

        public void Hide()
        {
            _isWorking = false;
            var args = new LaserPointerEventArgs
            {
                FromHand = _hand,
                Distance = 0f,
                Target = _previousContact,
            };
            OnRaycastBase(false, 0);
            OnPointerUpBase(args);
            OnPointerOutBase(args);
            _previousContact = null;
            onPointerDeactivated.Invoke();
        }

        #endregion

        private void ProcessPointer(bool hasHit, RaycastHit hit)
        {
            if (_previousContact && _previousContact != hit.transform)
            {
                var args = new LaserPointerEventArgs
                {
                    FromHand = _hand,
                    Distance = 0f,
                    Target = _previousContact
                };
                OnPointerOutBase(args);
                _previousContact = null;
            }

            if (hasHit && _previousContact != hit.transform)
            {
                var argsIn = new LaserPointerEventArgs
                {
                    FromHand = _hand,
                    Distance = hit.distance,
                    Target = hit.transform
                };
                OnPointerInBase(argsIn);
                _previousContact = hit.transform;
            }

            if (hasHit && _hand.Controller.TriggerButtonState.JustActivated)
            {
                var argsClick = new LaserPointerEventArgs
                {
                    FromHand = _hand,
                    Distance = hit.distance,
                    Target = hit.transform
                };
                OnPointerDownBase(argsClick);
            }

            if (hasHit && _hand.Controller.TriggerButtonState.JustDeactivated)
            {
                var argsClick = new LaserPointerEventArgs
                {
                    FromHand = _hand,
                    Distance = hit.distance,
                    Target = hit.transform
                };
                OnPointerUpBase(argsClick);
                OnPointerClickBase(argsClick);
            }

            OnPointerClickHoldBase(_hand.Controller.TriggerButtonState.Active);

            if (!hasHit)
            {
                _previousContact = null;
            }
        }

        #region PointerEvents

        protected virtual void OnRaycastBase(bool hasHit, float distance)
        {
            OnRaycast.Invoke(hasHit, distance);
        }

        private void OnPointerInBase(LaserPointerEventArgs e)
        {
            if (e.Target == null) return;
            onPointerIn.Invoke(e);
        }

        private void OnPointerClickBase(LaserPointerEventArgs e)
        {
            if (e.Target == null) return;
            onPointerClick.Invoke(e);
        }
        
        private void OnPointerClickHoldBase(bool isHolding)
        {
            onPointerClickHold.Invoke(isHolding);
        }

        private void OnPointerDownBase(LaserPointerEventArgs e)
        {
            if (e.Target == null) return;
            onPointerDown.Invoke(e);
        }

        private void OnPointerUpBase(LaserPointerEventArgs e)
        {
            if (e.Target == null) return;
            onPointerUp.Invoke(e);
        }

        private void OnPointerOutBase(LaserPointerEventArgs e)
        {
            if (e.Target == null) return;
            onPointerOut.Invoke(e);
        }
        #endregion
    }
}