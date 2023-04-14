using MyBox;
using System.Collections;
using TMPro;
using UnityDevKit.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDevKit.UI.Toggles
{
    public class UniversalToggleController : MonoBehaviour
    {
        [Separator("VSync Toggle Button")]
        [SerializeField] private Button toggleButton;

        [Separator("On/Off TextFields")]
        [SerializeField] private TMP_Text onText;
        [SerializeField] private TMP_Text offText;

        [Separator("Tringle Sprite")]
        [SerializeField] private Image triangleSprite;

        [Separator("Target On/Off Colors")]
        [SerializeField] private Color onBaseColor = new Color(0.385f, 0.77f, 0.32f, 1);
        [SerializeField] private Color offBaseColor = new Color(1, 0.01f, 0, 1);
        [SerializeField] private Color disabledBaseColor = new Color(0, 1, 1, 0.1f);

        [Separator("Button Transition Duration")]
        [SerializeField, PositiveValueOnly] private float buttonTransitionDuration = 0.5f;

        [Separator("Target Z Rotation")]
        [SerializeField] private float targetOnRotation;
        [SerializeField] private float targetOffRotation;

        private int _currentToggleValue = 0;

        private Vector3 _offButtonRotation;
        private Vector3 _onButtonRotation;

        private Transform _cachedButtonTransform;

        public EventHolder<int> OnToggleValueChanged = new EventHolder<int>();

        private void Awake()
        {
            onText.color = onBaseColor;

            _cachedButtonTransform = toggleButton.transform;

            _onButtonRotation = new Vector3(0, 0, targetOnRotation);
            _offButtonRotation = new Vector3(0, 0, targetOffRotation);
        }

        //private void Start()
        //{
        //    SetValueWithoutEvent(QualitySettings.vSyncCount);
        //}

        public void SetValueWithoutEvent(int value)
        {
            _currentToggleValue = value;

            if (value == 0)
            {
                StartCoroutine(ButtonRotationLerp(_offButtonRotation));
            }
            else if (value == 1)
            {
                StartCoroutine(ButtonRotationLerp(_onButtonRotation));
            }
            ToggleTextColor();
        }

        public int GetToggleValue()
        {
            return _currentToggleValue;
        }

        public void Toggle()
        {
            if (_currentToggleValue == 0)
            {
                _currentToggleValue = 1;
                StartCoroutine(ButtonRotationLerp(_onButtonRotation));
            }
            else if (_currentToggleValue == 1)
            {
                _currentToggleValue = 0;
                StartCoroutine(ButtonRotationLerp(_offButtonRotation));
            }
            ToggleTextColor();
            OnToggleValueChanged.Invoke(_currentToggleValue);
        }

        private void ToggleTextColor()
        {
            if (_currentToggleValue == 1)
            {
                onText.color = onBaseColor;
                offText.color = disabledBaseColor;

                triangleSprite.color = onBaseColor;
            }
            else if (_currentToggleValue == 0)
            {
                onText.color = disabledBaseColor;
                offText.color = offBaseColor;

                triangleSprite.color = offBaseColor;
            }
        }

        private IEnumerator ButtonRotationLerp(Vector3 targetRotation)
        {
            toggleButton.interactable = false;

            float deltaTime = 0;
            var startButtonTransform = _cachedButtonTransform.eulerAngles;

            while (deltaTime < buttonTransitionDuration)
            {
                deltaTime += Time.unscaledDeltaTime;
                var progress = deltaTime / buttonTransitionDuration;
                _cachedButtonTransform.localEulerAngles = Vector3.Lerp(startButtonTransform, targetRotation, progress);

                yield return null;
            }

            toggleButton.interactable = true;
        }

    }
}