using System;
using System.Collections;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Utils.Environment
{
    public class DynamicEnvironmentLight : MonoBehaviour
    {
        [Separator("Settings")]
        [SerializeField] [PositiveValueOnly] private float refreshPeriod = 30*60;
        [SerializeField] private AnimationCurve lightIntensityCurve;
        [SerializeField] private Light environmentLight;

        private float _lightIntensity;
        private TimeSpan _lastTime = TimeSpan.Zero;
        
        private void Start()
        {
            VirtualTimeHandler.Instance.OnTimeChange.AddListener(Refresh);
            InvokeRepeating(nameof(Refresh), refreshPeriod, refreshPeriod);
            _lightIntensity = environmentLight.intensity;
        }

        private void Refresh()
        {
            const int minHourDiff = 1;
            const float dayHours = 24f;
            var currentTime = VirtualTimeHandler.Instance.CurrentTime;
            
            if (Mathf.Abs((currentTime - _lastTime).Hours) < minHourDiff) return;
            
            var hour = currentTime.Hours;
            var hourRatio = hour / dayHours;

            var updatedIntensity = lightIntensityCurve.Evaluate(hourRatio) * _lightIntensity;
            StartCoroutine(ChangeLightIntensity(updatedIntensity));
            _lastTime = currentTime;
        }

        private IEnumerator ChangeLightIntensity(float intensity)
        {
            const int steps = 30;
            const float changeDuration = 1.5f;
            const float changeStepPeriod = changeDuration / steps;
            var intensityChangeAmount = (intensity - environmentLight.intensity) / steps; 
            for (var i = 0; i < steps; i++)
            {
                environmentLight.intensity += intensityChangeAmount;
                yield return new WaitForSeconds(changeStepPeriod);
            }
        }
    }
}