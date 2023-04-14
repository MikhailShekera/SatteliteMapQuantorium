using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using MyBox;
using UnityDevKit.Player.Controllers;
using SatelliteMap.Scripts.UniversalLogic.JsonLogic.ReaderWriter;

namespace SatelliteMap.Scripts.UniversalLogic.UniversalControllers.Sensitivity
{
    [RequireComponent(typeof(SensitivityReaderWriterLink))]
    public class MouseSensitivityController : MonoBehaviour
    {
        [Separator("Core")]
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text scrollbarDisplayValue;

        [SerializeField] private InputSensitivityController sensitivityController;

        private SensitivityReaderWriterLink _sensitivityReaderWriterLink;

        private void Start()
        {
            _sensitivityReaderWriterLink = GetComponent<SensitivityReaderWriterLink>();

            slider.onValueChanged.AddListener(UpdateScrollbarDisplayValue);
            slider.onValueChanged.AddListener(_sensitivityReaderWriterLink.RewriteData);

            slider.value = _sensitivityReaderWriterLink.ScriptableObject.data;
        }

        private void UpdateScrollbarDisplayValue(float currentValue)
        {
            scrollbarDisplayValue.text = Math.Round(currentValue, 0).ToString();
            sensitivityController.MouseSensivity = currentValue;
        }
    }
}