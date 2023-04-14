using UnityDevKit.Effects.Highlight.Managers;

namespace UnityDevKit.Effects.Highlight.Loaders
{
    public class InteractablePropertiesLoader : BasePropertiesLoader<HighlightEffectProperties>
    {
        public override HighlightEffectProperties GetProperties() =>
            BaseEffectsManager
                .Instance
                .GetHolder()
                .InteractableEffect;
    }
}