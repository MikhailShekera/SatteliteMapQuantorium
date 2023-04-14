using System;
using UnityEngine;

namespace UnityDevKit.Variants
{
    [RequireComponent(typeof(IVariantSwapper))]
    public abstract class VariantSwapperBaseExtension : MonoBehaviour
    {
        private IVariantSwapper _swapper;
        
        private void Awake()
        {
            _swapper = GetComponent<IVariantSwapper>();
        }

        private void Start()
        {
            _swapper.SubscribeOnSwap(GetSwapListener());
        }

        protected abstract Action GetSwapListener();
    }
}