using UnityEngine;

namespace UnityDevKit.Laser
{
    [CreateAssetMenu(fileName = "LaserData", menuName = "Laser", order = 0)]
    public sealed class LaserData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float thickness = 0.005f;
        [SerializeField] private float clickThickness = 0.02f;
        [SerializeField] private Material commonMaterial;
        [SerializeField] private Material clickMaterial;
        
        public GameObject Prefab => prefab;
        public float Thickness => thickness;
        public float ClickThickness => clickThickness;
        public Material CommonMaterial => commonMaterial;
        public Material ClickMaterial => clickMaterial;
    }
}