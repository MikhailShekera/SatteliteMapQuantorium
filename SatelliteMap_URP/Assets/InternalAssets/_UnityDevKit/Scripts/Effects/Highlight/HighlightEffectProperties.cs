using MyBox;
using UnityEngine;

namespace UnityDevKit.Effects.Highlight
{
    [CreateAssetMenu(fileName = "HighlightEffectProperties", menuName = "Effects/Highlight", order = 0)]
    public class HighlightEffectProperties : ScriptableObject
    {
        [Separator("Rim power")]
        public float rimPower = 1f;
        public float rimPowerModifier = 1f;
        
        [Separator("Rim Intensity")]
        public float rimIntensity = 1f;
        public float rimIntensityModifier = 1.2f;
        
        [Separator("Color")]
        [ColorUsage(true, true)] public Color rimColor = Color.cyan;
    }
}