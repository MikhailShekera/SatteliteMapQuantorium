using MyBox;
using UnityDevkit.Utils.JsonData;
using UnityEngine;

namespace SatelliteMap.Scripts.UniversalLogic.JsonLogic.ScriptableObjects
{
    [CreateAssetMenu(fileName = "VsyncDataScriptableObject", menuName = "ScriptableObject/VsyncData")]
    public sealed class VsyncDataScriptableObject : JsonHolder<int>
    {
        [SerializeField] private RangedInt valueRange;
        public override bool Validate(int newData)
        {
            return newData <= valueRange.Max && newData >= valueRange.Min;
        }
    }
}