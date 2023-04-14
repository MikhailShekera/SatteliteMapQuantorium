using UnityEngine;

namespace UnityDevKit.UI.Screens
{
    public abstract class ScreenWithOptionsSelection : Screen
    {
        [SerializeField] protected ChoiceSelector choiceSelector;
        [SerializeField] private ScreenErrorVisualizer errorVisualizer;

        private void Start()
        {
            choiceSelector.Reset();
        }

        private void HandleErrorSelection()
        {
            errorVisualizer.FlashError();
        }

        public override void Show()
        {
            base.Show();
            choiceSelector.Reset();
        }

        public override IScreen UpAction()
        {
            choiceSelector.SelectPrevious();
            return this;
        }

        public override IScreen DownAction()
        {
            choiceSelector.SelectNext();
            return this;
        }

        public override IScreen ConfirmAction()
        {
            if (choiceSelector.IsCorrectChoice)
            {
                return GetNext();
            }

            HandleErrorSelection();
            return this;
        }
    }
}