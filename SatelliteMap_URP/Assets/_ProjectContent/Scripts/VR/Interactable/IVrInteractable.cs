using HurricaneVR.Framework.Core.Grabbers;

namespace SatelliteMap.VR.Interactable
{
    public interface IVrInteractable
    {
        void Focus(HVRHandGrabber handGrabber);
        void DeFocus(HVRHandGrabber handGrabber);
        void Interact(HVRHandGrabber handGrabber);
        void AfterInteract(HVRHandGrabber handGrabber);
    }
}