using System.Collections;
using MyBox;
using UnityDevkit.Animation;
using UnityEngine;
using Zenject;

namespace UnityDevKit.Utils.SceneLoader
{
    public class SceneLoaderAnimation : AutoSceneLoadingExtension
    {
        [Separator("Animation settings")]
        [SerializeField] private bool useCustomSpeed;
        [SerializeField, PositiveValueOnly, ConditionalField(nameof(useCustomSpeed))] private float customSpeed = 1.5f;

        private BlinkAnimation _blinkAnimation;

        private void Start()
        {
            _blinkAnimation.OpenEyes();
        }

        [Inject]
        private void Construct(BlinkAnimation blinkAnimation)
        {
            _blinkAnimation = blinkAnimation;
        }

        public override IEnumerator Execute()
        {
            yield return useCustomSpeed
                ? _blinkAnimation.CloseEyesProcess(customSpeed)
                : _blinkAnimation.CloseEyesProcess();
        }
    }
}