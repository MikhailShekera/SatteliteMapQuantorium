using HurricaneVR.Framework.Core.Grabbers;
using UnityDevKit.Events;
using UnityEngine;

namespace SatelliteMap.VR.Interactable
{
    public class VrInteractableBase : MonoBehaviour, IVrInteractable
    {
        public readonly EventHolder<HVRHandGrabber> OnFocus = new EventHolder<HVRHandGrabber>();
        public readonly EventHolder<HVRHandGrabber> OnDeFocus = new EventHolder<HVRHandGrabber>();
        public readonly EventHolder<HVRHandGrabber> OnInteract = new EventHolder<HVRHandGrabber>();
        public readonly EventHolder<HVRHandGrabber> OnAfterInteract = new EventHolder<HVRHandGrabber>();
        
        public virtual void Focus(HVRHandGrabber handGrabber)
        {
            OnFocus.Invoke(handGrabber);
        }

        public virtual void DeFocus(HVRHandGrabber handGrabber)
        {
            OnDeFocus.Invoke(handGrabber);
        }

        public virtual void Interact(HVRHandGrabber handGrabber)
        {
            OnInteract.Invoke(handGrabber);
        }

        public virtual void AfterInteract(HVRHandGrabber handGrabber)
        {
            OnAfterInteract.Invoke(handGrabber);
        }
    }
}