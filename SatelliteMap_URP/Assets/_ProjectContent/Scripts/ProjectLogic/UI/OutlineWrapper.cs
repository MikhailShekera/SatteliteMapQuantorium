using MyBox;
using UnityDevKit.Effects;
using UnityEngine;

namespace UnityDevKit.UI
{
    [RequireComponent(typeof(UIOutline))]
    public class OutlineWrapper : BaseEffect, IMutableEffect
    {
        [Separator("Outline colors")]
        [SerializeField] private Color focusColor = new Color32(236, 162, 37, 255);
        [SerializeField] private Color grabbedColor = new Color32(85, 198, 87, 255);

        [Separator("Outline widths")]
        [SerializeField, Range(0f, 500f)] private float focusWidth = 20;
        [SerializeField, Range(0f, 500f)] private float grabbedWidth = 60;

        private UIOutline outline;

        public UIOutline Outline => outline;

        private void Awake()
        {
            outline = GetComponent<UIOutline>();
        }

        protected override void ApplyEffect()
        {
            outline.enabled = true;
            outline.color = focusColor;
            outline.OutlineWidth = focusWidth;
        }

        protected override void RemoveEffect()
        {
            outline.enabled = false;
        }

        public void IncreaseEffectPower()
        {
            outline.enabled = true;
            outline.color = grabbedColor;
            outline.OutlineWidth = grabbedWidth;
        }

        public void DecreaseEffectPower()
        {
            outline.enabled = true;
            outline.color = grabbedColor;
            outline.OutlineWidth = grabbedWidth;
        }
    }
}