using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityDevKit.Interactables.Impact
{
    public class ObjectImpactSound : MonoBehaviour
    {
        [Separator("Settings")] 
        [SerializeField, PositiveValueOnly] private float relativeVelocity = 1;
        [SerializeField, PositiveValueOnly] private float soundPeriod = 0.15f;
        [SerializeField] private AudioSource impactSound;
        [SerializeField, PositiveValueOnly] private float pithMaxDeltaAmount = 0.2f;

        private float _basePitch;
        private float _lastSoundTime;

        private void Awake()
        {
            _basePitch = impactSound.pitch;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (Time.time - _lastSoundTime < soundPeriod)
            {
                return;
            }

            if (collision.relativeVelocity.magnitude > relativeVelocity)
            {
                impactSound.pitch = _basePitch * (1 + Random.Range(-pithMaxDeltaAmount, pithMaxDeltaAmount));
                impactSound.Play();
            }

            _lastSoundTime = Time.time;
        }
    }
}