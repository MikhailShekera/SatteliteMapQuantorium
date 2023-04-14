using UnityDevKit.Effects.ObjectOutline;

namespace SatelliteMap.VR.RayPointer.Extensions
{
    public class VrRayInteractableUiHighlighter : VrRayPointerExtension
    {
        protected override void OnPointerIn(LaserPointerEventArgs e)
        {
            base.OnPointerIn(e);
            var outlineWrapper = e.Target.GetComponentInChildren<ObjectOutlineEffect>();
            if (outlineWrapper)
            {
                outlineWrapper.Apply();
            }
        }

        protected override void OnPointerOut(LaserPointerEventArgs e)
        {
            base.OnPointerOut(e);
            var outlineWrapper = e.Target.GetComponentInChildren<ObjectOutlineEffect>();
            if (outlineWrapper)
            {
                outlineWrapper.Remove();
            }
        }

        protected override void OnPointerDown(LaserPointerEventArgs e)
        {
            base.OnPointerClick(e);
            var outlineWrapper = e.Target.GetComponentInChildren<ObjectOutlineEffect>();
            if (outlineWrapper)
            {
                outlineWrapper.IncreaseEffectPower();
            }
        }

        protected override void OnPointerUp(LaserPointerEventArgs e)
        {
            base.OnPointerClick(e);
            var outlineWrapper = e.Target.GetComponentInChildren<ObjectOutlineEffect>();
            if (outlineWrapper)
            {
                outlineWrapper.DecreaseEffectPower();
            }
        }
    }
}