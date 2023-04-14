using UnityDevKit.UI;

namespace SatelliteMap.VR.RayPointer.Extensions
{
    public class VrRayInteractableOutline : VrRayPointerExtension
    {
        protected override void OnPointerIn(LaserPointerEventArgs e)
        {
            base.OnPointerIn(e);
            if (e.Target.TryGetComponent(out OutlineWrapper outlineWrapper))
            {
                outlineWrapper.Apply();
            }
        }

        protected override void OnPointerOut(LaserPointerEventArgs e)
        {
            base.OnPointerOut(e);
            if (e.Target.TryGetComponent(out OutlineWrapper outlineWrapper))
            {
                outlineWrapper.Remove();
            }
        }
        protected override void OnPointerDown(LaserPointerEventArgs e)
        {
            base.OnPointerClick(e);
            if (e.Target.TryGetComponent(out OutlineWrapper outlineWrapper))
            {
                outlineWrapper.IncreaseEffectPower();
            }
        }
        
        protected override void OnPointerUp(LaserPointerEventArgs e)
        {
            base.OnPointerClick(e);
            if (e.Target.TryGetComponent(out OutlineWrapper outlineWrapper))
            {
                outlineWrapper.DecreaseEffectPower();
            }
        }
    }
}