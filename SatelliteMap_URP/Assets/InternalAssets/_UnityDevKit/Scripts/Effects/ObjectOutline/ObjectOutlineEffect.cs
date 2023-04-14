using UnityEngine;

namespace UnityDevKit.Effects.ObjectOutline
{
    public class ObjectOutlineEffect : BaseEffect, IMutableEffect
    {
        [SerializeField] private GameObject highlightBorderObject;
        [SerializeField] private MeshRenderer highlightBorderRenderer;
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material increasedMaterial;
        
        protected override void ApplyEffect()
        {
            highlightBorderObject.SetActive(true);
            highlightBorderRenderer.sharedMaterial = highlightMaterial;
        }

        protected override void RemoveEffect()
        {
            highlightBorderObject.SetActive(false);
        }

        public void IncreaseEffectPower()
        {
            highlightBorderRenderer.sharedMaterial = increasedMaterial;
        }

        public void DecreaseEffectPower()
        {
            highlightBorderRenderer.sharedMaterial = highlightMaterial;
        }
    }
}