using MyBox;
using UnityDevKit.Effects;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityEditor;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Interactable.Extensions
{
    public abstract class InteractableEffect<TEffect> : InteractableExtension
        where TEffect : MonoBehaviour, IMutableEffect
    {
        [SerializeField] private TEffect effect;

        public TEffect Effect => effect;

        protected override void Init()
        {
            base.Init();
            EffectSetup();
        }

        // protected override void OnActiveStateChangedAction(bool isActive)
        // {
        //     base.OnActiveStateChangedAction(isActive);
        //     ToggleOutline(isActive);
        // }

        #region Effect methods

        private void EffectSetup()
        {
            if (!effect)
            {
                if (!TryGetComponent(out effect))
                {
                    Debug.LogError("You have to setup effect reference");
                }
            }
        }

        private void ToggleEffect(bool isEnabled)
        {
            if (isEnabled)
            {
                ApplyEffect();
            }

            RemoveEffect();
        }

        protected void ApplyEffect()
        {
            effect.Apply();
        }

        protected void RemoveEffect()
        {
            effect.Remove();
        }

        protected void IncreaseOutline()
        {
            effect.IncreaseEffectPower();
        }

        protected void DecreaseOutline()
        {
            effect.DecreaseEffectPower();
        }

        #endregion

        #region Extension methods

        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            ApplyEffect();
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            RemoveEffect();
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            IncreaseOutline();
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            DecreaseOutline();
        }

        #endregion

#if UNITY_EDITOR
        [ButtonMethod]
        public void AddEffect()
        {
            var currentGameObject = gameObject;
            effect = currentGameObject.AddComponent<TEffect>();
            //currentGameObject.AddComponent<InteractablePropertiesLoader>();
            EditorUtility.SetDirty(currentGameObject);
        }
#endif
    }
}