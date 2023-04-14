using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityDevKit.Events;
using UnityEngine;

namespace UnityDevKit.Variants
{
    public class VariantSwapper<TVariant> : MonoBehaviour, IVariantSwapper
    where TVariant : Variant
    {
        [SerializeField] [InitializationField] protected TVariant startVariant;
        [SerializeField] [InitializationField] private bool turnOnStartVariant;
        [SerializeField] [InitializationField] private List<TVariant> variants;
        [SerializeField] [InitializationField] private bool loadVariantsFromChildren = true;

        private readonly EventHolder<TVariant> _onVariantActivate = new();
        private readonly EventHolder<TVariant> _onVariantChanged = new();
        private readonly EventHolder<TVariant> _onSameVariantSelected = new();
        
        public List<TVariant> Variants => variants;

        public TVariant CurrentActive { get; protected set; }

        protected virtual void Start()
        {
            if (loadVariantsFromChildren)
            {
                variants = GetComponentsInChildren<TVariant>().ToList();
            }

            foreach (var variant in variants)
            {
                variant.OnTurnOn.AddListener(() => Activate(variant));
            }
            
            ActivateDefault();
        }

        protected void Activate(TVariant variant)
        {
            
            if (CurrentActive == variant)
            {
                _onSameVariantSelected.Invoke(CurrentActive);
                _onVariantActivate.Invoke(CurrentActive);
                return;
            }
            if (CurrentActive != null)
            {
                CurrentActive.TurnOff();
            }
            CurrentActive = variant;
            _onVariantChanged.Invoke(CurrentActive);
            _onVariantActivate.Invoke(CurrentActive);
        }

        public void ActivateDefault()
        {
            if (turnOnStartVariant)
            {
                startVariant.TurnOn();
            }
            Activate(startVariant);
        }
        
        public void SubscribeOnActivate(EventHolder<TVariant>.EventHandler listener)
        {
            _onVariantActivate.AddListener(listener);
        }

        public void SubscribeOnSwap(EventHolder<TVariant>.EventHandler listener)
        {
            _onVariantChanged.AddListener(listener);
        }
        
        public void SubscribeOnSameSelected(EventHolder<TVariant>.EventHandler listener)
        {
            _onSameVariantSelected.AddListener(listener);
        }

        public void SubscribeOnSwap(Action listener)
        {
            _onVariantChanged.AddListener(_ => listener());
        }
    }
}