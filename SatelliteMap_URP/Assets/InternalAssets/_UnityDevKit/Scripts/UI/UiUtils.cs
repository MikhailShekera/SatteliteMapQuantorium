using System.Collections;
using TMPro;
using UnityEngine;

namespace UnityDevKit.UI
{
    public static class UiUtils
    {
        public static IEnumerator ShowForTime(TMP_Text textHolder, string showText, string defaultText, float showTime)
        {
            textHolder.text = showText;
            yield return new WaitForSeconds(showTime);
            textHolder.text = defaultText;
        }
    }
}