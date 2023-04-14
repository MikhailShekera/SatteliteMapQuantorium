using MyBox;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SatelliteMap.Scripts.UniversalLogic.UniversalControllers.Volume
{
    [Serializable]
    public struct VolumeControllerParameters
    {
        public Slider volumeSlider;
        public AudioMixerGroup audioMixerGroup;

        public bool overwriteSliderValues;
        [ConditionalField(nameof(overwriteSliderValues))]
        public float minValue;
        [ConditionalField(nameof(overwriteSliderValues))]
        public float maxValue;

        public bool hasCheckSound;
        [ConditionalField(nameof(hasCheckSound))]
        public AudioSource checkSound;

        public bool hasDisplayValue;
        [ConditionalField(nameof(hasDisplayValue))]
        public TMP_Text displayValue;
    }
}