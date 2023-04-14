using System;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Audio
{
    [Serializable]
    public sealed class ToggledAudioSource
    {
        [SerializeField] private bool useAudio;
        [SerializeField, ConditionalField(nameof(useAudio))] private AudioSource audioSource;

        public AudioSource AudioSource => audioSource;

        public void Play()
        {
            if (!useAudio) return;
            audioSource.Play();
        }
    }
}