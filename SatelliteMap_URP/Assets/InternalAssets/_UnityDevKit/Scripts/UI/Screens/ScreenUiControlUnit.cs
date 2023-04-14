using MyBox;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDevKit.UI.Screens
{
    [DisallowMultipleComponent]
    public class ScreenUiControlUnit : MonoBehaviour
    {
        [SerializeField] private ScreenController screenController;

        [Separator("Buttons")] 
        [SerializeField] private Button powerButton;
        [SerializeField] private Button enterButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;
        [SerializeField] private Button startStopButton;

        private void Start()
        {
            powerButton.onClick.AddListener(TriggerAction);
            enterButton.onClick.AddListener(MainAction);
            backButton.onClick.AddListener(SecondaryAction);
            upButton.onClick.AddListener(UpAction);
            downButton.onClick.AddListener(DownAction);
            startStopButton.onClick.AddListener(StartStopAction);
        }

        private void MainAction()
        {
            screenController.ConfirmAction();
        }

        private void SecondaryAction()
        {
            screenController.BackAction();
        }

        private void TriggerAction()
        {
            screenController.PowerAction();
        }

        private void UpAction()
        {
            screenController.UpAction();
        }

        private void DownAction()
        {
            screenController.DownAction();
        }
        
        private void StartStopAction()
        {
            screenController.StartStopAction();
        }
    }
}