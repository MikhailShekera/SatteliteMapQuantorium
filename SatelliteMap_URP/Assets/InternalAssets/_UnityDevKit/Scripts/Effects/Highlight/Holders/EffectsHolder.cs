using MyBox;
using UnityEngine;

namespace UnityDevKit.Effects.Highlight.Holders
{
    [CreateAssetMenu(fileName = "BaseEffectsHolder", menuName = "Effects/Holder/Base", order = 0)]
    public class EffectsHolder : ScriptableObject
    {
        [DisplayInspector] [SerializeField] private HighlightEffectProperties interactableEffect;
        public HighlightEffectProperties InteractableEffect => interactableEffect;
    }
}