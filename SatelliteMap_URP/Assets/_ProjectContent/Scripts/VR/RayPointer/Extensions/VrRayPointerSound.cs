using MyBox;
using UnityEngine;

namespace SatelliteMap.VR.RayPointer.Extensions
{
    public class VrRayPointerSound : VrRayPointerExtension
    {
        [Separator("Sound")] 
        [SerializeField] private AudioSource startSound;
        [SerializeField] private AudioSource loopSound;
        [SerializeField] private AudioSource stopSound;

        protected override void OnPointerActivated()
        {
            const float loopSoundDelay = 0.1f;
            base.OnPointerActivated();
            startSound.Play();
            loopSound.PlayDelayed(loopSoundDelay);
        }

        protected override void OnPointerDeactivated()
        {
            base.OnPointerDeactivated();
            loopSound.Stop();
            stopSound.Play();
        }
    }
}