using MyBox;
using UnityEngine;

namespace UnityDevKit.Effects.Highlight.Managers
{
    public abstract class EffectsManager<TEffectsHolder, TEffectsManager> : Singleton<TEffectsManager>
    where TEffectsHolder : ScriptableObject
    where TEffectsManager : MonoBehaviour
    {
        [DisplayInspector] [SerializeField] private TEffectsHolder effectsHolder;

        public TEffectsHolder GetHolder() => effectsHolder;
    }
}
