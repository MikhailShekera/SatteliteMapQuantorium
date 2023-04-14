using SatelliteMap.Scripts.UniversalLogic.JsonLogic.ReaderWriter;
using UnityDevKit.UI.Toggles;
using UnityDevKit.Utils;
using UnityEngine;

namespace SatelliteMap.UI
{
    [RequireComponent(typeof(UniversalToggleController))]
    [RequireComponent(typeof(VsyncReaderWriterLink))]
    public class VSyncToggle : MonoBehaviour
    {
        [SerializeField] private bool instantSet;

        private UniversalToggleController _toggle;
        private VsyncReaderWriterLink _vsyncReaderWriterLink;

        private void Start()
        {
            _toggle = GetComponent<UniversalToggleController>();
            _vsyncReaderWriterLink = GetComponent<VsyncReaderWriterLink>();

            _toggle.SetValueWithoutEvent(_vsyncReaderWriterLink.ScriptableObject.data);

            _toggle.OnToggleValueChanged.AddListener(_vsyncReaderWriterLink.RewriteData);

            if (instantSet)
            {
                _toggle.OnToggleValueChanged.AddListener(VSyncController.SetVSyncValue);
                _toggle.OnToggleValueChanged.Invoke(_vsyncReaderWriterLink.ScriptableObject.data);
            }
        }
    }
}
