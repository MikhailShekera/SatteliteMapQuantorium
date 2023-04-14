using UnityEngine;

namespace UnityDevKit.Animation
{
    public static class AnimationUtils
    {
        public static float GetAnimatorClipLenght(this Animator animator, int clipNumber) =>
            animator.GetCurrentAnimatorClipInfo(0)[clipNumber].clip.length;
        
        public static float GetAnimatorStateLenght(this Animator animator) =>
            animator.GetCurrentAnimatorStateInfo(0).length;
    }
}