using System;
using MyBox;

namespace UnityDevKit.UI.Screens
{
    [Serializable]
    public class ChoiceSelector
    {
        public Choice[] choiceUnits;
        [PositiveValueOnly] public int startSelectionIndex;
        
        private int _currentChoiceIndex;

        private Choice CurrentChoice => choiceUnits[_currentChoiceIndex];

        public ChoiceSelector()
        {
            _currentChoiceIndex = startSelectionIndex;
        }
        
        private void Select(int choiceIndex)
        {
            CurrentChoice.Obj.SetActive(false);
            _currentChoiceIndex = choiceIndex;
            CurrentChoice.Obj.SetActive(true);
        }
        
        public void Reset()
        {
            if (choiceUnits.IsNullOrEmpty())
            {
                throw new NullReferenceException("There's no choice unit");
            }
            Select(startSelectionIndex);
        }

        public void SelectPrevious()
        {
            var newIndex = _currentChoiceIndex == 0 ? choiceUnits.Length - 1 : _currentChoiceIndex - 1;
            Select(newIndex);
        }

        public void SelectNext()
        {
            var newIndex = _currentChoiceIndex == choiceUnits.Length - 1 ? 0 : _currentChoiceIndex + 1;
            Select(newIndex);
        }

        public bool IsCorrectChoice => CurrentChoice.IsCorrect;
    }
}