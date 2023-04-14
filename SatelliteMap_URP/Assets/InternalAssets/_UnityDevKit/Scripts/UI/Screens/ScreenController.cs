using MyBox;
using UnityEngine;

namespace UnityDevKit.UI.Screens
{
    public class ScreenController : MonoBehaviour
    {
        [Separator("Settings")] 
        [SerializeField] private Screen startScreen;
        [SerializeField] [InitializationField] protected bool isPowered;
        
        private IScreen _currentScreen;

        private void Start()
        {
            _currentScreen = startScreen;
        }
        
        private void UpdateScreen(IScreen screen)
        {
            if (screen == _currentScreen) return;
            _currentScreen.Hide();
            screen.Show();
            _currentScreen = screen;
        }

        public virtual void PowerAction()
        {
            isPowered = !isPowered;
            UpdateScreen(startScreen);
            _currentScreen.PowerAction(isPowered);
        }

        public void UpAction()
        {
            if (!isPowered) return;
            var newScreen = _currentScreen.UpAction();
            UpdateScreen(newScreen);
        }

        public void DownAction()
        {
            if (!isPowered) return;
            var newScreen = _currentScreen.DownAction();
            UpdateScreen(newScreen);
        }

        public void ConfirmAction()
        {
            if (!isPowered) return;
            var newScreen = _currentScreen.ConfirmAction();
            UpdateScreen(newScreen);
        }

        public void BackAction()
        {
            if (!isPowered) return;
            var newScreen = _currentScreen.BackAction();
            UpdateScreen(newScreen);
        }
        
        public void StartStopAction()
        {
            if (!isPowered) return;
            var newScreen = _currentScreen.StartStopAction();
            UpdateScreen(newScreen);
        }
    }
}