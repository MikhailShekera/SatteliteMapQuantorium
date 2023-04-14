using UnityDevKit.Audio;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Source.Extensions
{
    public class InteractionSourceSound : InteractionSourceExtension
    {
        [SerializeField] private ToggledAudioSource onFocusAudioSource;
        [SerializeField] private ToggledAudioSource onDeFocusAudioSource;
        [SerializeField] private ToggledAudioSource onInteractAudioSource;
        [SerializeField] private ToggledAudioSource onAfterInteractAudioSource;
        
        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            onFocusAudioSource.Play();
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            onDeFocusAudioSource.Play();
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            onInteractAudioSource.Play();
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            onAfterInteractAudioSource.Play();
        }
    }
}