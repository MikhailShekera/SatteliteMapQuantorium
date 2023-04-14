using HurricaneVR.Framework.Core.Grabbers;

namespace SatelliteMap.VR.Interactable.Extensions
{
    public class VrInteractableLaserToggle : VrInteractableExtension
    {
        protected override void OnFocusAction(HVRHandGrabber handGrabber)
        {
            base.OnFocusAction(handGrabber);
            if (handGrabber.TryGetComponent(out UnityDevKit.Laser.Laser laser))
            {
                laser.Show();
            }
        }

        protected override void OnDeFocusAction(HVRHandGrabber handGrabber)
        {
            base.OnDeFocusAction(handGrabber);
            if (handGrabber.TryGetComponent(out UnityDevKit.Laser.Laser laser))
            {
                laser.Hide();
            }
        }

        protected override void OnInteractAction(HVRHandGrabber handGrabber)
        {
            base.OnInteractAction(handGrabber);
            if (handGrabber.TryGetComponent(out UnityDevKit.Laser.Laser laser))
            {
                laser.Increase();
            }
        }

        protected override void OnAfterInteractAction(HVRHandGrabber handGrabber)
        {
            base.OnAfterInteractAction(handGrabber);
            if (handGrabber.TryGetComponent(out UnityDevKit.Laser.Laser laser))
            {
                laser.Decrease();
            }
        }
    }
}