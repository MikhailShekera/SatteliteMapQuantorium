using MyBox;
using UnityEngine;
using UnityEngine.Events;
using UnityDevKit.Optimization;

namespace UnityDevKit.Interactables.Spinnable
{
    public class SpinnableObject : CachedMonoBehaviour
    {
        [Separator("Spin settings")]
        [SerializeField] private float spinSpeed = 20f;

        [Separator("Axis settings")]
        [SerializeField] protected bool localAxis = true;
        [SerializeField] protected bool xRotation;
        [SerializeField] protected bool yRotation;
        [SerializeField] protected bool zRotation;

        [SerializeField] private UnityEvent onDragEvent;

        private float _currentSpeed;

        protected virtual void Start()
        {
            TurnSpinningOn();
        }

        private void OnMouseDrag()
        {
            var spinX = Input.GetAxis("Mouse X") * _currentSpeed;
            var spinY = Input.GetAxis("Mouse Y") * _currentSpeed;
            onDragEvent.Invoke();
            ExecuteSpin(spinX, spinY);
        }

        protected void ExecuteSpin(float spinX, float spinY)
        {
            if (localAxis)
            {
                SpinOnLocalAxis(spinX, spinY);
            }
            else
            {
                SpinOnGlobalAxis(spinX, spinY);
            }
        }

        protected virtual void SpinOnLocalAxis(float spinX, float spinY)
        {
            if (xRotation) TransformData.Rotate(Vector3.right, -spinX, Space.Self);
            if (yRotation) TransformData.Rotate(Vector3.up, spinY, Space.Self);
            if (zRotation) TransformData.Rotate(Vector3.forward, spinX, Space.Self);
        }

        protected virtual void SpinOnGlobalAxis(float spinX, float spinY)
        {
            if (xRotation) TransformData.Rotate(Vector3.right, -spinX, Space.World);
            if (yRotation) TransformData.Rotate(Vector3.up, spinY, Space.World);
            if (zRotation) TransformData.Rotate(Vector3.forward, spinX, Space.World);
        }

        private void TurnSpinningOn()
        {
            _currentSpeed = spinSpeed;
        }

        public void TurnSpinningOff()
        {
            _currentSpeed = 0f;
        }
    }
}