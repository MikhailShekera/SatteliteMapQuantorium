using System.Collections;
using UnityDevKit.Animation;
using UnityEngine;

namespace UnityDevkit.Animation
{
    public class BlinkAnimation : MonoBehaviour
    {
        [SerializeField] private Animator blinkAnimator;

        private float _baseSpeed;
        
        private static readonly int IsBlinkHash = Animator.StringToHash("IsBlink");

        private void Awake()
        {
            _baseSpeed = blinkAnimator.speed;
        }

        public IEnumerator CloseEyesProcess()
        {
            CloseEyes();
            yield return new WaitForSeconds(blinkAnimator.GetAnimatorStateLenght());
        }
        
        public IEnumerator CloseEyesProcess(float speed)
        {
            blinkAnimator.speed = speed;
            CloseEyes();
            yield return new WaitForSeconds(blinkAnimator.GetAnimatorStateLenght() / speed);
            yield return new WaitForEndOfFrame();
            Reset();
        }
        
        public IEnumerator OpenEyesProcess()
        {
            OpenEyes();
            yield return new WaitForSeconds(blinkAnimator.GetAnimatorStateLenght());
        }
        
        public void CloseEyes()
        {
            blinkAnimator.SetBool(IsBlinkHash, true);
        }

        public void OpenEyes()
        {
            blinkAnimator.SetBool(IsBlinkHash, false);
        }

        private void Reset()
        {
            blinkAnimator.speed = _baseSpeed;
        }
    }
}