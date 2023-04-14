using UnityEngine.Rendering.Universal;

namespace UnityDevKit.Utils.PostProcess
{
    public sealed class VignetteEffectTransition : EffectFloatParameterTransition<Vignette>
    {
        protected override float GetEffectParameterValue() => Effect.intensity.value;
        
        protected override void OverrideEffectParameterValue(float value)
        {
            Effect.intensity.Override(value);
        }
    }
}