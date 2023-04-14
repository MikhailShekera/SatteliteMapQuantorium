using System.Collections;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Effects.Highlight
{
    public class SmoothHighlightEffect : HighlightEffect
    {
        [Separator("Smooth settings")]
        [SerializeField] [PositiveValueOnly] private float increasedEffectTime = 0.75f;

        private Coroutine _coroutine;
        
        public override void IncreaseEffectPower()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                SetCommonEffectPower();
            }
            _coroutine = StartCoroutine(SmoothIncreaseProcess());
        }

        private IEnumerator SmoothIncreaseProcess()
        {
            var time = increasedEffectTime / 2f;
            var startIntensity = GetEffectPower();
            var increasedIntensity = properties.rimIntensity * properties.rimIntensityModifier;
            yield return SmoothPowerChangeProcess(startIntensity, increasedIntensity, time);
            yield return SmoothPowerChangeProcess(increasedIntensity, startIntensity, time);
        }
        
        private IEnumerator SmoothPowerChangeProcess(float startIntensity, float increasedIntensity, float time)
        {
            var currentTime = 0f;
            while (currentTime < time)
            {
                var currentIntensity =
                    Mathf.Lerp(startIntensity, increasedIntensity, currentTime / time);
                SetRimIntensity(currentIntensity);
                currentTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}