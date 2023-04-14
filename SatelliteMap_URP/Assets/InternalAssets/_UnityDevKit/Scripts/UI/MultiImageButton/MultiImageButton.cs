using UnityEngine;
using UnityEngine.UI;

namespace UnityDevKit.UI.MultiImageButton
{
    [RequireComponent(typeof(MultipleComponentsColorTint))]
    public class MultiImageButton : Button
    {
        private MultipleComponentsColorTint targetGraphics;

        protected override void Awake()
        {
            base.Awake();
            targetGraphics = GetComponent<MultipleComponentsColorTint>();
        }

        protected override void Start()
        {
            base.Start();
            DoStateTransition(SelectionState.Normal, true);
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            var targetColor =
                state switch
                {
                    SelectionState.Disabled => colors.disabledColor,
                    SelectionState.Highlighted => colors.highlightedColor,
                    SelectionState.Normal => colors.normalColor,
                    SelectionState.Pressed => colors.pressedColor,
                    SelectionState.Selected => colors.selectedColor,
                    _ => Color.white
                };

            foreach (var graphic in targetGraphics.GetTargetGraphics)
                graphic.CrossFadeColor(targetColor, instant ? 0 : colors.fadeDuration, true, true);
        }
    }
}