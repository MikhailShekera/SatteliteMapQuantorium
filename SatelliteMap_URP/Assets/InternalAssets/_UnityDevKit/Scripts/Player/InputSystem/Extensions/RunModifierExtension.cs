using System.Collections;
using MyBox;
using UnityDevKit.Player.Extensions;
using UnityEngine;

namespace UnityDevKit.Player.InputSystem
{
    public class RunModifierExtension : MonoBehaviour
    {
        [Separator("Run Modifier Parameters")] 
        [SerializeField, Range(1, 10)] private int runModifierPriority = 1;
        [SerializeField] private RunModifierChangeData modifierChangeData = new RunModifierChangeData
        {
            TargetModifier = 1f,
            TimeDelta = 1f
        };

        protected UniversalPlayerMovement MainMovementComponent;

        private static int currentPriority;

        private const float DEFAULT_DELTA = 0.5f;
        private const int DEFAULT_PRIORITY = -1;

        protected virtual void Awake()
        {
            var component = GetComponentInParent<UniversalPlayerMovement>();
            if (component != null)
            {
                MainMovementComponent = component;
            }
            else
            {
                throw new System.ArgumentNullException("Main script not found");
            }
        }

        protected bool HandleRunModifier(RunModifierType modifierType)
        {
            if (!HasHigherPriority) return false;
            StopCoroutine(nameof(UpdateRunModifierProcess));
            UpdateRunModifier(modifierType);
            currentPriority = runModifierPriority;
            return true;
        }

        protected bool HandleDefaultRunModifier()
        {
            if (!HasHigherPriority) return false;
            StopCoroutine(nameof(UpdateRunModifierProcess));
            SetDefaultRunModifier();
            currentPriority = DEFAULT_PRIORITY;
            return true;
        }

        private bool HasHigherPriority => currentPriority <= runModifierPriority;

        private void SetDefaultRunModifier()
        {
            StartCoroutine(UpdateRunModifierProcess(
                MainMovementComponent.DefaultMovementModifier,
                DEFAULT_DELTA));
        }

        private void UpdateRunModifier(RunModifierType _)
        {
            StartCoroutine(UpdateRunModifierProcess(
                modifierChangeData.TargetModifier,
                modifierChangeData.TimeDelta));
        }

        private IEnumerator UpdateRunModifierProcess(float targetRunModifier, float timeDelta)
        {
            float elapsedTime = 0;
            var startRunModifier = MainMovementComponent.RunModifier;
            while (elapsedTime < timeDelta)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / timeDelta;
                MainMovementComponent.RunModifier = Mathf.Lerp(startRunModifier, targetRunModifier, progress);
                yield return null;
            }
        }
    }
}