using System;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public class InteractableSound : InteractableExtension
    {
        [SerializeField] private bool useOnFocusSound;

        [SerializeField] [ConditionalField(nameof(useOnFocusSound))]
        private AudioSource onFocusAudioSource;

        [SerializeField] private bool useOnInteractSound;

        [SerializeField] [ConditionalField(nameof(useOnInteractSound))]
        private AudioSource onInteractAudioSource;

        protected override void OnFocusAction(GameObject source)
        {
            if (!useOnFocusSound) return;
            onFocusAudioSource.Play();
        }

        protected override void OnInteractAction(GameObject source)
        {
            if (!useOnInteractSound) return;
            onInteractAudioSource.Play();
        }
    }
}