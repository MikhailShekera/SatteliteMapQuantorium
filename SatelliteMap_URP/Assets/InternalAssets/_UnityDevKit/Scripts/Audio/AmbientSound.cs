using System.Collections;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AmbientSound : MonoBehaviour
    {
        [SerializeField] private RangedFloat minMaxValues = new RangedFloat(0, 1);

        private AudioSource _audioSource;

        private Coroutine _currentCoroutine;

        private const int VOLUME_CHANGE_ITERATIONS = 10;
        private const float VOLUME_CHANGE_TIME_DELTA = 0.1f;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetVolumeBounds(RangedFloat bounds)
        {
            minMaxValues = bounds;
        }

        public void SetVolume(float volume)
        {
            var normalizedVolume = NormalizeVolume(volume);

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(SmoothVolumeChange(normalizedVolume));
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public float GetClipDuration() => _audioSource.clip.length;

        private IEnumerator SmoothVolumeChange(float targetVolume)
        {
            var currentVolume = _audioSource.volume;
            var volumeChangeDelta = (targetVolume - currentVolume) / VOLUME_CHANGE_ITERATIONS;

            for (var i = 0; i < VOLUME_CHANGE_ITERATIONS; i++)
            {
                _audioSource.volume += volumeChangeDelta;
                yield return new WaitForSeconds(VOLUME_CHANGE_TIME_DELTA);
            }
        }

        private float NormalizeVolume(float volume) =>
            Mathf.Abs(volume - minMaxValues.Min) / (minMaxValues.Max - minMaxValues.Min);
    }
}