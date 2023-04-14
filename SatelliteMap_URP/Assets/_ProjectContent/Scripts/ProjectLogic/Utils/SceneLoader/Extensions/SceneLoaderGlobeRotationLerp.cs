using MyBox;
using System.Collections;
using UnityDevKit.Interactables.Spinnable;
using UnityDevKit.Utils.SceneLoader;
using UnityEngine;

namespace SatelliteMap.Utils
{
    public class SceneLoaderGlobeRotationLerp : AutoSceneLoadingExtension
    {
        [Separator("Globes")]
        [SerializeField] private GameObject mainGlobe;
        [SerializeField] private GameObject targetGlobe;

        [Separator("Lerp Duration")]
        [SerializeField, PositiveValueOnly] private float lerpDuration = 2.5f;

        [Separator("Rotation Component")]
        [SerializeField] private SpinObject rotationComponent;

        [Separator("Ambient Sound")]
        [SerializeField] private AudioSource ambientSource;

        public override IEnumerator Execute()
        {
            float elapsedTime = 0;
            Quaternion startRotation = mainGlobe.transform.rotation;

            while (elapsedTime < lerpDuration)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / lerpDuration;
                mainGlobe.transform.rotation = Quaternion.Lerp(startRotation, targetGlobe.transform.rotation, progress);
                ambientSource.volume = Mathf.Lerp(1, 0, progress);
                yield return null;
            }

            rotationComponent.enabled = false;
        }
    }
}