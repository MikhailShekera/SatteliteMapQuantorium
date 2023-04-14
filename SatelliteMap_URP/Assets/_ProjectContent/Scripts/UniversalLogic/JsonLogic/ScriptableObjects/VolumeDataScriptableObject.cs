using MyBox;
using SatelliteMap.Scripts.UniversalLogic.UniversalControllers.Volume;
using System.Collections.Generic;
using System.Linq;
using UnityDevkit.Utils.JsonData;
using UnityEngine;

namespace SatelliteMap.Scripts.UniversalLogic.JsonLogic.ScriptableObjects
{
    [CreateAssetMenu(fileName = "VolumeDataScriptableObject", menuName = "ScriptableObject/VolumeData")]
    public sealed class VolumeDataScriptableObject : JsonHolder<List<MixerValuesParameters<float>>>
    {
        [SerializeField] private RangedFloat valueRange;

        public override bool Validate(List<MixerValuesParameters<float>> validationData)
        {
            return ValidateDataLength(validationData) && ValidateValueRange(validationData);

        }

        #region LocalValidations
        private bool ValidateDataLength(List<MixerValuesParameters<float>> validationData)
        {
            return defaultScriptableObject.data.Count == validationData.Count;
        }

        private bool ValidateValueRange(List<MixerValuesParameters<float>> validationData)
        {
            return !validationData.Any(vData => vData.value > valueRange.Max && vData.value < valueRange.Min);
        }
        #endregion LocalValidations
    }
}