using System;
using MyBox;
using UnityDevKit.Effects.Highlight;
using UnityEngine;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public class InteractableHighlighter : InteractableEffect<HighlightEffect>
    {
        [SerializeField] [InitializationField] private bool isSelectable;
        [SerializeField] [InitializationField] private bool isSelected;
        [SerializeField] private HighlightEffect selectedEffect;

        private bool IsLocked => isSelectable && isSelected;

        protected override void Init()
        {
            base.Init();
            if (IsLocked)
            {
                selectedEffect.Apply();
                selectedEffect.IncreaseEffectPower();
            }
        }
        
        public void UnSelect()
        {
            isSelected = false;
            RemoveEffect();
            selectedEffect.Remove();
        }
        
        protected override void OnFocusAction(GameObject source)
        {
            if (IsLocked)
            {
                return;
            }
            base.OnFocusAction(source);
        }

        protected override void OnDeFocusAction(GameObject source)
        {
            if (IsLocked)
            {
                return;
            }
            base.OnDeFocusAction(source);
        }
        
        protected override void OnInteractAction(GameObject source)
        {
            base.OnInteractAction(source);
            if (!isSelectable)
            {
                return;
            }

            if (!isSelected)
            {
                isSelected = true;
                selectedEffect.Apply();
            }
            
            selectedEffect.IncreaseEffectPower();
        }
    }
}