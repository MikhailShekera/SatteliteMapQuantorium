using System;

namespace UnityDevKit.Variants
{
    public interface IVariantSwapper
    {
        void SubscribeOnSwap(Action listener);
    }
}