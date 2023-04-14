using SatelliteMap.VR.Interactable;

namespace SatelliteMap.VR.RayPointer.Extensions
{
    public class VrRayPointerInteraction : VrRayPointerExtension
    {
        protected override void OnPointerIn(LaserPointerEventArgs e)
        {
            base.OnPointerIn(e);
            if (e.Target.TryGetComponent(out IVrInteractable interactable))
            {
                interactable.Focus(e.FromHand);
            }
        }

        protected override void OnPointerOut(LaserPointerEventArgs e)
        {
            base.OnPointerOut(e);
            if (e.Target.TryGetComponent(out IVrInteractable interactable))
            {
                interactable.DeFocus(e.FromHand);
            }
        }
        protected override void OnPointerDown(LaserPointerEventArgs e)
        {
            base.OnPointerClick(e);
            if (e.Target.TryGetComponent(out IVrInteractable interactable))
            {
                interactable.Interact(e.FromHand);
            }
        }
        
        protected override void OnPointerUp(LaserPointerEventArgs e)
        {
            base.OnPointerClick(e);
            if (e.Target.TryGetComponent(out IVrInteractable interactable))
            {
                interactable.AfterInteract(e.FromHand);
            }
        }
    }
}