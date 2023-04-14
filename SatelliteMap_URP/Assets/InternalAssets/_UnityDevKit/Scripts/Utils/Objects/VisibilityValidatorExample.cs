using System;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Utils.Objects
{
    public class VisibilityValidatorExample : MonoBehaviour
    {
        [SerializeField] private VisibilityValidatorWithState visibilityValidator;

        [SerializeField] private Transform observer;
        [SerializeField] private Transform validationObject;

        [ReadOnly] private bool _isVisible;

        private Coroutine _currentValidatingCoroutine;
        
        private void Start()
        {
            ValidateInInterval();
        }
        
        private void ValidateInInterval()
        {
            if (_currentValidatingCoroutine != null)
            {
                StopCoroutine(_currentValidatingCoroutine);
            }
            _currentValidatingCoroutine = StartCoroutine(
                visibilityValidator.StartValidating(observer, validationObject));
        }

        private void StopValidating()
        {
            if (_currentValidatingCoroutine != null)
            {
                StopCoroutine(_currentValidatingCoroutine);
            }
        }

        private void Update()
        {
            _isVisible = visibilityValidator.IsVisible;

            if (Input.GetKeyUp(KeyCode.V))
            {
                ValidateInInterval();
            }
            if (Input.GetKeyUp(KeyCode.B))
            {
                StopValidating();
            }
        }
    }
}