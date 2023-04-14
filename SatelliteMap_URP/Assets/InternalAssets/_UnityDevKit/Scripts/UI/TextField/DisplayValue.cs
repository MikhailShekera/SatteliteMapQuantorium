using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UnityDevKit.UI.TextField
{
    [Serializable]
    public abstract class DisplayValue<T>
    {
        [SerializeField] private string beforeValueText;
        [SerializeField] protected TMP_Text displayText;

        public void SendToTextField(T value)
        {
            displayText.text = BuildText(value);
        }

        public void SendToTextField(string value)
        {
            displayText.text = value;
        }

        public IEnumerator ShowForTime(T showValue, T defaultValue, float showTime)
        {
            var showText = BuildText(showValue);
            var defaultText = BuildText(defaultValue);
            yield return UiUtils.ShowForTime(displayText, showText, defaultText, showTime);
        }
        
        public IEnumerator ShowForTime(string showText, string defaultText, float showTime)
        {
            yield return UiUtils.ShowForTime(displayText, showText, defaultText, showTime);
        }

        protected abstract string ConvertToString(T value);

        private string BuildText(T value)
        {
            var convertedText = ConvertToString(value);
            return string.IsNullOrEmpty(beforeValueText)
                ? convertedText
                : $"{beforeValueText}{convertedText}";
        }
    }
}