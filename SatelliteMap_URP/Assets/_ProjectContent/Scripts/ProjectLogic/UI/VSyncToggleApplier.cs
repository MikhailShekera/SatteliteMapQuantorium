using UnityDevKit.UI.Toggles;
using UnityDevKit.Utils;
using UnityEngine;

namespace SatelliteMap.UI
{
    public sealed class VSyncToggleApplier : MonoBehaviour
    {
        [SerializeField] private UniversalToggleController vSyncValueController;

        public void ApplyCurrentValue()
        {
            var currentValue = vSyncValueController.GetToggleValue();
            VSyncController.SetVSyncValue(currentValue);
        }
    }
}