using System;
using MyBox;
using UnityDevKit.Effects;
using UnityDevKit.Effects.Highlight.Loaders;
using UnityEditor;
using UnityEngine;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public abstract class InteractableEffect<TEffect> : InteractableExtension
    where TEffect : MonoBehaviour, IMutableEffect 
    {
        [SerializeField] private TEffect effect;

        public TEffect Effect => effect;
        
        protected override void Init()
        {
            base.Init();
            OutlineSetup();
        }

        protected override void OnActiveStateChangedAction(bool isActive)
        {
            base.OnActiveStateChangedAction(isActive);
            ToggleOutline(isActive);
        }

        // ----- OUTLINE -----
        private void OutlineSetup()
        {
            if (effect == null)
            {
                effect = GetComponent<TEffect>();
            }

            if (effect == null)
            {
                throw new InteractableException();
            }
        }

        private void ToggleOutline(bool isEnabled)
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

        // ----- INTERACT -----
        protected override void OnFocusAction(GameObject source)
        {
            base.OnFocusAction(source);
            ApplyEffect();
        }

        protected override void OnDeFocusAction(GameObject source)
        {
            base.OnDeFocusAction(source);
            RemoveEffect();
        }

        protected override void OnInteractAction(GameObject source)
        {
            base.OnInteractAction(source);
            IncreaseOutline();
        }

        protected override void OnAfterInteractAction(GameObject source)
        {
            base.OnAfterInteractAction(source);
            DecreaseOutline();
        }

#if UNITY_EDITOR
        [ButtonMethod]
        public void AddEffect()
        {
            var currentGameObject = gameObject;
            effect = currentGameObject.AddComponent<TEffect>();
            currentGameObject.AddComponent<InteractablePropertiesLoader>();
            EditorUtility.SetDirty(currentGameObject);
        }
#endif
    }

}