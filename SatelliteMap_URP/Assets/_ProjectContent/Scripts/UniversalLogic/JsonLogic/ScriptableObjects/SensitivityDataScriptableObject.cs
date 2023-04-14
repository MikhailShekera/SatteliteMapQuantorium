using MyBox;
using UnityDevkit.Utils.JsonData;
using UnityEngine;

namespace SatelliteMap.Scripts.UniversalLogic.JsonLogic.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SensitivityDataScriptableObject", menuName = "ScriptableObject/SensitivityData")]
    public sealed class SensitivityDataScriptableObject : JsonHolder<float>
    {
        [SerializeField] private RangedFloat valueRange;

        public override bool Validate(float newData)
        {
            return !(newData > valueRange.Max) && !(newData < valueRange.Min);
        }
    }
}
