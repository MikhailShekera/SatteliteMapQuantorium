using MyBox;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Source.Extensions
{
    public class InteractionSourceWorkingSound : InteractionSourceExtension
    {
        [Separator("Sound")] 
        [SerializeField] private AudioSource startSound;
        [SerializeField] private AudioSource loopSound;
        [SerializeField] private AudioSource stopSound;

        protected override void OnActivateStateChangedAction(bool isActivated)
        {
            base.OnActivateStateChangedAction(isActivated);
            if (isActivated)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }

        private void Activate()
        {
            const float loopSoundDelay = 0.1f;
            startSound.Play();
            loopSound.PlayDelayed(loopSoundDelay);
        }

        private void Deactivate()
        {
            loopSound.Stop();
            stopSound.Play();
        }
    }
}