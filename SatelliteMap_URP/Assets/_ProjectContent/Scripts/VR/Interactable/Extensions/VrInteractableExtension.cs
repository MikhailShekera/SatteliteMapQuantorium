using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace SatelliteMap.VR.Interactable.Extensions
{
    [RequireComponent(typeof(VrInteractableBase))]
    public abstract class VrInteractableExtension : MonoBehaviour
    {
        protected VrInteractableBase Interactable;

        protected virtual void Awake()
        {
            Interactable = GetComponent<VrInteractableBase>();
        }

        private void Start()
        {
            InteractableEventsInit();
        }

        private void InteractableEventsInit()
        {
            Interactable.OnFocus.AddListener(OnFocusAction);
            Interactable.OnDeFocus.AddListener(OnDeFocusAction);
            Interactable.OnInteract.AddListener(OnInteractAction);
            Interactable.OnAfterInteract.AddListener(OnAfterInteractAction);
        }

        protected virtual void OnFocusAction(HVRHandGrabber value)
        {
        }

        protected virtual void OnDeFocusAction(HVRHandGrabber value)
        {
        }

        protected virtual void OnInteractAction(HVRHandGrabber value)
        {
        }

        protected virtual void OnAfterInteractAction(HVRHandGrabber value)
        {
        }
    }
}