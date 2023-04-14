using MyBox;
using UnityDevKit.ComponentWrapper;
using UnityEngine;

namespace UnityDevKit.Effects.MaterialChange
{
    public class MaterialChangeEffect : BaseEffect
    {
        [Separator("Object settings")] 
        [SerializeField] [InitializationField]
        private ComponentHolder<MeshRenderer> meshRendererHolder;

        [Separator("Effect settings")] 
        [SerializeField] private Material[] effectMaterials;

        private MeshRenderer _meshRenderer;
        private Material[] _startMaterials;

        private void Start()
        {
            _meshRenderer = meshRendererHolder.WrappedComponent;
            _startMaterials = _meshRenderer.sharedMaterials;
        }

        protected override void ApplyEffect()
        {
            _meshRenderer.sharedMaterials = effectMaterials;
        }

        protected override void RemoveEffect()
        {
            _meshRenderer.sharedMaterials = _startMaterials;
        }
    }
}