using MyBox;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Utils.SceneLoader
{
    public class SceneLoaderCameraLerp : AutoSceneLoadingExtension
    {
        [Separator("Cameras")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera targetCamera;

        [Separator("Lerp Duration")]
        [SerializeField, PositiveValueOnly] private float lerpDuration = 1.5f;
        
        [Separator("Events")]
        [SerializeField] private UnityEvent onLerpStart;
        [SerializeField] private UnityEvent onLerpEnd;

        public override IEnumerator Execute()
        {
            onLerpStart.Invoke();
            var elapsedTime = 0f;
            var mainCameraTransform = mainCamera.transform;
            var startPosition = mainCameraTransform.position;
            var startRotation = mainCameraTransform.rotation;

            var targetCameraTransform = targetCamera.transform;
            while (elapsedTime < lerpDuration)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / lerpDuration;
                mainCameraTransform.position = Vector3.Lerp(startPosition, targetCameraTransform.position, progress);
                mainCameraTransform.rotation = Quaternion.Lerp(startRotation, targetCameraTransform.rotation, progress);
                yield return null;
            }

            onLerpEnd.Invoke();
        }
    }
}