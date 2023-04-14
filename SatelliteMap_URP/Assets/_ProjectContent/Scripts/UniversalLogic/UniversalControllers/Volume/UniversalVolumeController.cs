using MyBox;
using SatelliteMap.Scripts.UniversalLogic.JsonLogic.ReaderWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace SatelliteMap.Scripts.UniversalLogic.UniversalControllers.Volume
{
    [RequireComponent(typeof(VolumeMixerReaderWriterLink))]
    public class UniversalVolumeController : MonoBehaviour
    {
        [Separator("Audio Mixer")]
        [SerializeField] private AudioMixer audioMixer;

        [Separator("Audio Mixer Groups List")]
        [SerializeField] private List<VolumeControllerParameters> controllerParameters;

        private VolumeMixerReaderWriterLink _volumeMixerReader;

        private readonly List<MixerValuesParameters<float>> _localParametersHolder = new List<MixerValuesParameters<float>>();

        private void Start()
        {
            _volumeMixerReader = GetComponent<VolumeMixerReaderWriterLink>();

            SetSliders();
            LoadParameters();
        }

        private void SetSliders()
        {
            foreach (var parameter in controllerParameters)
            {
                _localParametersHolder.Add(
                    new MixerValuesParameters<float>
                    {
                        name = parameter.audioMixerGroup.name,
                        value = parameter.volumeSlider.value
                    });

                if (parameter.overwriteSliderValues)
                {
                    parameter.volumeSlider.minValue = parameter.minValue;
                    parameter.volumeSlider.maxValue = parameter.maxValue;
                }

                parameter.volumeSlider.onValueChanged.AddListener(value => SetMixerValue(value, parameter));

                parameter.volumeSlider.onValueChanged.AddListener(value => RewriteParameter(value, parameter.audioMixerGroup.name));
            }
        }

        private void LoadParameters()
        {
            foreach(var parameter in _volumeMixerReader.ScriptableObject.data)
            {
                var targetParameter = controllerParameters.FirstOrDefault(data => parameter.name == data.audioMixerGroup.name);
                targetParameter.volumeSlider.value = parameter.value;
            }
        }

        private void SetMixerValue(float value, VolumeControllerParameters volumeControllerParameters)
        {
            audioMixer.SetFloat(volumeControllerParameters.audioMixerGroup.name, (float)(Math.Log10(value) * 20f));

            if (volumeControllerParameters.hasDisplayValue)
            {
                volumeControllerParameters.displayValue.text = Math.Round(value * 100, 0).ToString();
            }

            if (volumeControllerParameters.hasCheckSound)
            {
                volumeControllerParameters.checkSound.Play();
            }
        }

        private void RewriteParameter(float value, string parameterName)
        {
            var targetParameter = _localParametersHolder.FirstOrDefault(data => parameterName == data.name);
            targetParameter!.value = value;
            _volumeMixerReader.RewriteData(_localParametersHolder);
        }
    }
}