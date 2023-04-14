using UnityEngine;

namespace UnityDevKit.Variants
{
    [RequireComponent(typeof(VariantSwapper<>))]
    public abstract class VariantSwapperExtension<TSwapper, TVariant> : MonoBehaviour
    where TSwapper : VariantSwapper<TVariant>
    where TVariant : Variant
    {
        protected TSwapper Swapper;
        
        private void Awake()
        {
            Swapper = GetComponent<TSwapper>();
        }

        private void Start()
        {
            SubscribeOnEvents();
        }

        protected virtual void SubscribeOnEvents()
        {
            Swapper.SubscribeOnSwap(OnVariantChange);
            Swapper.SubscribeOnSameSelected(OnSameVariantSelected);
        }

        protected abstract void OnVariantChange(TVariant variant);
        protected abstract void OnSameVariantSelected(TVariant variant);
    }
}