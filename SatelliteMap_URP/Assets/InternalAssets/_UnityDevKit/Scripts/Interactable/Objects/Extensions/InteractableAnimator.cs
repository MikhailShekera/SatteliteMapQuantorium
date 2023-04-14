using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Interactables.Extensions
{
    [RequireComponent(typeof(Animator))] [Obsolete("Class is obsolete", false)]
    public class InteractableAnimator : InteractableExtension
    {
        [SerializeField] private UnityEvent onAnimationPlay;
        [SerializeField] private UnityEvent onAnimationOpen;
        [SerializeField] private UnityEvent onAnimationClose;
        [SerializeField] private UnityEvent afterCloseAnim;
        [SerializeField] private UnityEvent afterOpenAnim;

        private Animator _animator;
        private static readonly int IsOpen = Animator.StringToHash("Interacted");

        private bool _isOpen;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            _isOpen = _animator.GetBool(IsOpen);
        }

        protected override void OnInteractAction(GameObject source)
        {
            base.OnInteractAction(source);
            Toggle();
        }

        public void Toggle()
        {
            _isOpen = !_isOpen;
            _animator.SetBool(IsOpen, _isOpen);
            StartCoroutine(ToggleCoroutine());
        }

        private IEnumerator ToggleCoroutine()
        {
            while (_animator.GetCurrentAnimatorClipInfo(0).Length == 0)
            {
                yield return null;
            }

            onAnimationPlay.Invoke();

            if (_isOpen)
            {
                onAnimationOpen.Invoke();
                var clipLength = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
                Invoke(nameof(InvokeAfterOpenAnim), clipLength);
            }
            else
            {
                var clipLength = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
                Invoke(nameof(InvokeAfterCloseAnim), clipLength);
                onAnimationClose.Invoke();
            }
        }

        private void InvokeAfterOpenAnim()
        {
            afterOpenAnim.Invoke();
        }

        private void InvokeAfterCloseAnim()
        {
            afterCloseAnim.Invoke();
        }
    }
}