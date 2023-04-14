using HurricaneVR.Framework.Core.Grabbers;
using UnityDevKit.Interactables;
using UnityEngine;

namespace SatelliteMap.VR.Interactable.Extensions
{
    [RequireComponent(typeof(InteractableBase))]
    public class VrInteractableConnector : VrInteractableExtension
    {
        private InteractableBase _interactable;
        
        protected override void Awake()
        {
            base.Awake();
            _interactable = GetComponent<InteractableBase>();
        }

        protected override void OnFocusAction(HVRHandGrabber handGrabber)
        {
            base.OnFocusAction(handGrabber);
            _interactable.Focus();
        }

        protected override void OnDeFocusAction(HVRHandGrabber handGrabber)
        {
            base.OnDeFocusAction(handGrabber);
            _interactable.DeFocus();
        }

        protected override void OnInteractAction(HVRHandGrabber handGrabber)
        {
            base.OnInteractAction(handGrabber);
            _interactable.Interact();
        }
        
        protected override void OnAfterInteractAction(HVRHandGrabber handGrabber)
        {
            base.OnAfterInteractAction(handGrabber);
            _interactable.AfterInteract();
        }
    }
}