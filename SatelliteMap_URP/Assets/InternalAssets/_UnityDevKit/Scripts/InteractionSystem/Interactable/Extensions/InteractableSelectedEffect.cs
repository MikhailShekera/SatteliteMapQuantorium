using MyBox;
using UnityDevKit.Effects;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Interactable.Extensions
{
    public abstract class InteractableSelectedEffect<TEffect> : InteractableEffect<TEffect>
        where TEffect : MonoBehaviour, IMutableEffect
    {
        [SerializeField] [InitializationField] private bool isSelectable;
        [SerializeField] [InitializationField] private bool isSelected;
        [SerializeField] private TEffect selectedEffect;

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
        
        protected override void OnFocusAction(InteractionArgs args)
        {
            if (IsLocked)
            {
                return;
            }
            base.OnFocusAction(args);
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            if (IsLocked)
            {
                return;
            }
            base.OnDeFocusAction(args);
        }
        
        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
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