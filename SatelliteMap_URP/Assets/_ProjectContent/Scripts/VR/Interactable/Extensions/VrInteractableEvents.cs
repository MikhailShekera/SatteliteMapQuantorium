using HurricaneVR.Framework.Core.Grabbers;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

namespace SatelliteMap.VR.Interactable.Extensions
{
    public class VrInteractableEvents : VrInteractableExtension
    {
        [Separator("Events")] 
        [SerializeField] private UnityEvent onFocusEvent;
        [SerializeField] private UnityEvent onDeFocusEvent;
        [SerializeField] private UnityEvent onInteractEvent;
        [SerializeField] private UnityEvent onAfterInteractEvent;
        
        protected override void OnFocusAction(HVRHandGrabber handGrabber)
        {
            base.OnFocusAction(handGrabber);
            onFocusEvent.Invoke();
        }

        protected override void OnDeFocusAction(HVRHandGrabber handGrabber)
        {
            base.OnDeFocusAction(handGrabber);
            onDeFocusEvent.Invoke();
        }

        protected override void OnInteractAction(HVRHandGrabber handGrabber)
        {
            base.OnInteractAction(handGrabber);
            onInteractEvent.Invoke();
        }
        
        protected override void OnAfterInteractAction(HVRHandGrabber handGrabber)
        {
            base.OnAfterInteractAction(handGrabber);
            onAfterInteractEvent.Invoke();
        }
    }
}