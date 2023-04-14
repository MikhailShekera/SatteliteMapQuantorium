using System.Collections;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDevKit.UI.Screens
{
    public class ScreenErrorVisualizer : MonoBehaviour
    {
        [SerializeField] private Image panel;
        [SerializeField] private Color errorColor = Color.red;
        [SerializeField] private float errorDuration = 0.25f;
        [SerializeField] [PositiveValueOnly] private int errorFlashCount = 2;

        private Color _baseColor;
        private Coroutine _currentCoroutine;

        private void Start()
        {
            _baseColor = panel.color;
        }

        public void FlashError()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(FlashErrorProcess());
        }

        private IEnumerator FlashErrorProcess()
        {
            for (var i = 0; i < errorFlashCount; i++)
            {
                panel.color = errorColor;
                yield return new WaitForSeconds(errorDuration);
                panel.color = _baseColor;
                yield return new WaitForSeconds(errorDuration);
            }
        }
    }
}