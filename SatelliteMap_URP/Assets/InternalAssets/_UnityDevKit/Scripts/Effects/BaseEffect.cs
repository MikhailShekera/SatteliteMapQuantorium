using UnityEngine;

namespace UnityDevKit.Effects
{
    public abstract class BaseEffect : MonoBehaviour, IEffect
    {
        [SerializeField] protected BaseEffect[] childrenEffects;

        public virtual void Apply()
        {
            ApplyEffect();
            foreach (var child in childrenEffects)
            {
                child.Apply();
            }
        }

        public virtual void Remove()
        {
            RemoveEffect();
            foreach (var child in childrenEffects)
            {
                child.Remove();
            }
        }

        protected abstract void ApplyEffect();
        protected abstract void RemoveEffect();
    }
}
