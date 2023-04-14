using System;
using System.Collections;
using MyBox;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityDevKit.Utils.PostProcess
{
    public abstract class EffectFloatParameterTransition<TEffect> : MonoBehaviour
    where TEffect : VolumeComponent, IPostProcessComponent
    {
        [Separator("Volume profile")]
        [SerializeField] private VolumeProfile volumeProfile;
        
        [Separator("Transition settings")]
        [SerializeField] private float defaultIntensity = 0f;
        [SerializeField] private float customIntensity = 0.3f;
        [SerializeField] private float transitionTime = 0.35f;
        
        protected TEffect Effect;
        
        private float _maxDelta;

        private const int VOLUME_PRIORITY = 1;
        
        private void Awake()
        {
            LoadEffect();
            UpsertVolume();
            _maxDelta = Math.Abs(customIntensity - defaultIntensity);
        }
        
        public void SetDefault()
        {
            StopAllCoroutines();
            StartCoroutine(SmoothChange(defaultIntensity));
        }

        public void SetCustom()
        {
            StopAllCoroutines();
            StartCoroutine(SmoothChange(customIntensity));
        }
        
        protected abstract float GetEffectParameterValue();
        
        protected abstract void OverrideEffectParameterValue(float value);

        private void LoadEffect()
        {
            if (!volumeProfile.TryGet(out Effect))
            {
                Effect = volumeProfile.Add<TEffect>();
            }
        }

        private void UpsertVolume()
        {
            if (!TryGetComponent(out Volume volume))
            {
                volume = gameObject.AddComponent<Volume>();
            }
            
            volume.priority = VOLUME_PRIORITY;
            volume.profile = volumeProfile;
            
            OverrideEffectParameterValue(defaultIntensity);
        }
        
        private IEnumerator SmoothChange(float targetIntensity)
        {
            var currentTime = 0f;
            var delta = Math.Abs(targetIntensity - GetEffectParameterValue());
            var ratio = Math.Abs(delta / _maxDelta);
            var currentTransitionTime = transitionTime * ratio;
            while (currentTime < currentTransitionTime)
            {
                var nextIntensity = Mathf.Lerp(GetEffectParameterValue(), targetIntensity, currentTime / currentTransitionTime);
                OverrideEffectParameterValue(nextIntensity);
                currentTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}