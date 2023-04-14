using MyBox;
using UnityDevKit.Events;
using UnityDevKit.InteractionSystem.Source;
using UnityDevKit.Player.Extensions;
using UnityDevKit.Player.UI;
using UnityDevKit.Utils.SceneLoader;
using UnityDevKit.Utils.ScenesHandlers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using Zenject;

namespace SatelliteMap.Menu
{
    public sealed class PauseMenuController : BindedPlayerExtension
    {
        [Separator("UI Part")]
        [SerializeField] private GameObject exitScreen;

        [Separator("Event Flow Json Save")]
        [SerializeField] private EventFlow jsonSaveEvent;

        [Separator("Player Crosshair")]
        [SerializeField] private GameObject crosshair;

        [Separator("Input System")]
        [SerializeField] private PlayerUiPointerInputModule playerUiPointerInputModule;
        [SerializeField] private InputSystemUIInputModule inputSystemUIInput;

        [SerializeField] private BaseInteractionSource[] interactionSources;

        private ISceneLoader _sceneLoader;
        private bool _isPaused = false;

        private InputAction _togglePauseMenuAction;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public override void AddBindings()
        {
            _togglePauseMenuAction = PlayerController.InputManager.MovementControls.Movement.TogglePauseMenu;

            _togglePauseMenuAction.started += ToggleExitMenu;
        }

        private void OnDestroy()
        {
            _togglePauseMenuAction.started -= ToggleExitMenu;
        }

        public void ToggleExitMenu(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                ToggleExitScreen();
            }
        }

        public void ConfirmExit()
        {
            _sceneLoader.Quit();
            ToggleExitScreen();
        }

        public void DenyExit()
        {
            ToggleExitScreen();
        }

        private void ToggleExitScreen()
        {
            _isPaused = !_isPaused;
            exitScreen.SetActive(_isPaused);
            crosshair.SetActive(!_isPaused);

            playerUiPointerInputModule.enabled = !_isPaused;
            inputSystemUIInput.enabled = _isPaused;

            if (_isPaused)
            {
                ToggleInteractions(false);
                CursorHandler.TurnOnCursor();
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                CursorHandler.TurnOffCursor();
                ToggleInteractions(true);
                jsonSaveEvent.Invoke();
            }
        }

        private void ToggleInteractions(bool isActive)
        {
            foreach (var interactionSource in interactionSources)
            {
                interactionSource.enabled = isActive;
            }
        }
    }
}