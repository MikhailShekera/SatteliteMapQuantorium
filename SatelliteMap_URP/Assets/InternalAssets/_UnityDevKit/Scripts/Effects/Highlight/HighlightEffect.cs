using MyBox;
using UnityDevKit.ComponentWrapper;
using UnityDevKit.Effects.Highlight.Loaders;
using UnityEngine;

namespace UnityDevKit.Effects.Highlight
{
    public class HighlightEffect : BaseEffect, IMutableEffect
    {
        [SerializeField] protected HighlightEffectProperties properties;

        [SerializeField] [InitializationField] private ComponentHolder<MeshRenderer> meshRendererHolder;

        private MeshRenderer _meshRenderer;
        private Material[] _highlightedMaterials;
        private Material[] _defaultMaterials;

        private static readonly int RimPowerID = Shader.PropertyToID("Vector1_51D2992B");
        private static readonly int RimIntensityID = Shader.PropertyToID("Vector1_9C013D6C");
        private static readonly int RimColorID = Shader.PropertyToID("Color_87739F");
        private static readonly int AlbedoID = Shader.PropertyToID("Texture2D_C389AB95");
        private static readonly int NormalId = Shader.PropertyToID("Texture2D_51375A76");
        private static readonly int MetallicID = Shader.PropertyToID("Texture2D_7C12B9C2");
        private static readonly int OcclusionID = Shader.PropertyToID("Texture2D_998385A3");
        
        private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
        private static readonly int BumpMap = Shader.PropertyToID("_BumpMap");
        private static readonly int MetallicGlossMap = Shader.PropertyToID("_MetallicGlossMap");
        private static readonly int OcclusionMap = Shader.PropertyToID("_OcclusionMap");

        private void Awake()
        {
            _meshRenderer = meshRendererHolder.WrappedComponent;
            if (_meshRenderer == null)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
            }

            if (_meshRenderer == null)
            {
                Debug.LogError("[HighlightEffect] There's no MeshRenderer component");
            }
            Init();
        }

        protected virtual void Start()
        {
            
        }

        protected override void ApplyEffect()
        {
            _meshRenderer.materials = _highlightedMaterials;
        }

        protected override void RemoveEffect()
        {
            _meshRenderer.materials = _defaultMaterials;
        }
        
        public virtual void IncreaseEffectPower()
        {
            IncreaseEffectPower(properties.rimPowerModifier, properties.rimIntensityModifier);
        }

        public virtual void DecreaseEffectPower()
        {
            DecreaseEffectPower(properties.rimPowerModifier, properties.rimIntensityModifier);
        }
        
        public void SetCommonEffectPower()
        {
            SetHighlightMaterialsFloat(RimIntensityID, properties.rimIntensity);
        }
        
        public void IncreaseEffectPower(float rimPowerModifier, float rimIntensityModifier)
        {
            SetHighlightMaterialsFloat(RimPowerID, properties.rimPower * rimPowerModifier);
            SetHighlightMaterialsFloat(RimIntensityID, properties.rimIntensity * rimIntensityModifier);
        }

        public void DecreaseEffectPower(float rimPowerModifier, float rimIntensityModifier)
        {
            SetHighlightMaterialsFloat(RimPowerID, properties.rimPower / rimPowerModifier);
            SetHighlightMaterialsFloat(RimIntensityID, properties.rimIntensity / rimIntensityModifier);
        }
        
        public void SetRimPower(float power)
        {
            SetHighlightMaterialsFloat(RimPowerID, power);
        }
        
        public void SetRimIntensity(float intensity)
        {
            SetHighlightMaterialsFloat(RimIntensityID, intensity);
        }
        
        public float GetEffectPower() => _highlightedMaterials[0].GetFloat(RimIntensityID);

        protected void Init()
        {
            _defaultMaterials = _meshRenderer.materials;

            HandleProperties();
            
            HighlightedMaterialInit();
        }

        private void HandleProperties()
        {
            if (properties != null) return;
            var loader = GetComponent<IPropertiesLoader<HighlightEffectProperties>>();
            properties = loader.GetProperties();
        }

        private void HighlightedMaterialInit()
        {
            _highlightedMaterials = new Material[_meshRenderer.materials.Length];
            for (var i = 0; i < _meshRenderer.materials.Length; i++)
            {
                var defaultMaterial = _defaultMaterials[i];
                var highlightedMaterial = new Material(Shader.Find("Shader Graphs/Highlight"))
                {
                    name = $"Highlighted {name}"
                };

                highlightedMaterial.SetFloat(RimPowerID, properties.rimPower);
                highlightedMaterial.SetFloat(RimIntensityID, properties.rimIntensity);
                highlightedMaterial.SetColor(RimColorID, properties.rimColor);

                highlightedMaterial.SetTexture(AlbedoID, defaultMaterial.GetTexture(BaseMap));
                highlightedMaterial.SetTexture(NormalId, defaultMaterial.GetTexture(BumpMap));
                highlightedMaterial.SetTexture(MetallicID, defaultMaterial.GetTexture(MetallicGlossMap));
                highlightedMaterial.SetTexture(OcclusionID, defaultMaterial.GetTexture(OcclusionMap));
                
                _highlightedMaterials[i] = highlightedMaterial;
            }
        }

        private void SetHighlightMaterialsFloat(int nameID, float value)
        {
            foreach (var highlightedMaterial in _highlightedMaterials)
            {
                highlightedMaterial.SetFloat(nameID, value);
            }
        }
    }
}