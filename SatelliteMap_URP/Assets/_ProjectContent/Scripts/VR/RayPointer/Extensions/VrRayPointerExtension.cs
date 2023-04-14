using UnityEngine;

namespace SatelliteMap.VR.RayPointer.Extensions
{
    [RequireComponent(typeof(VrRayPointer))]
    public abstract class VrRayPointerExtension : MonoBehaviour
    {
        protected VrRayPointer RayPointer;
        
        private void Awake()
        {
            RayPointer = GetComponent<VrRayPointer>();
            RayPointer.OnRaycast.AddListener(OnRaycast);
            RayPointer.OnPointerActivatedEvent.AddListener(OnPointerActivated);
            RayPointer.OnPointerDeactivatedEvent.AddListener(OnPointerDeactivated);
            RayPointer.OnPointerInEvent.AddListener(OnPointerIn);
            RayPointer.OnPointerClickEvent.AddListener(OnPointerClick);
            RayPointer.OnPointerClickHoldEvent.AddListener(OnPointerClickHold);
            RayPointer.OnPointerOutEvent.AddListener(OnPointerOut);
            RayPointer.OnPointerDownEvent.AddListener(OnPointerDown);
            RayPointer.OnPointerUpEvent.AddListener(OnPointerUp);
        }

        protected virtual void OnPointerActivated()
        {
        }
        
        protected virtual void OnPointerDeactivated()
        {
        }
        
        protected virtual void OnRaycast(bool hasHit, float distance)
        {
        }
        
        protected virtual void OnPointerIn(LaserPointerEventArgs args)
        {
        }
        
        protected virtual void OnPointerClick(LaserPointerEventArgs args)
        {
        }
        
        protected virtual void OnPointerClickHold(bool isHolding)
        {
        }
        
        protected virtual void OnPointerOut(LaserPointerEventArgs args)
        {
        }
        
        protected virtual void OnPointerDown(LaserPointerEventArgs args)
        {
        }
        
        protected virtual void OnPointerUp(LaserPointerEventArgs args)
        {
        }
    }
}