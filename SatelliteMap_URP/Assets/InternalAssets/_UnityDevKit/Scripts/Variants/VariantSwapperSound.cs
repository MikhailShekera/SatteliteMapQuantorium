using System;
using UnityEngine;

namespace UnityDevKit.Variants
{
    public class VariantSwapperSound : VariantSwapperBaseExtension
    {
        [SerializeField] private AudioSource swapSound;

        protected override Action GetSwapListener() => swapSound.Play;
    }
}