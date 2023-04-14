using MyBox;
using UnityDevKit.Utils.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using UnityDevKit.XR;
using Zenject;

namespace SatelliteMap.Menu
{
    public class StartMenuController : MonoBehaviour
    {
        [Separator("Scene loading")]
        [SerializeField] private string mainSceneName = "MainScene_XR";
        
        [Separator("Mode Buttons")]
        [SerializeField] private Image desktopButtonModeColor;
        [SerializeField] private Image vrButtonModeColor;

        [Separator("Menu Screens")]
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private GameObject soundSettingsScreen;

        private ISceneLoader _sceneLoader;
        private XrMode _xrMode;

        private readonly Color _pickedModeColor = new Color(0.01375042f, 0.9716981f, 0.4432033f, 0.3607843f);
        private readonly Color _defaultColor = new Color(0, 0.9960785f, 0.9960785f, 0.1058824f);

        private void Start()
        {
            SetDesktopMode();

            QualitySettings.vSyncCount = 1;
        }

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        #region ButtonEvents
        public void LoadScene()
        {
            _sceneLoader.LoadScene(mainSceneName, _xrMode);
        }

        public void Exit()
        {
            _sceneLoader.Quit();
        }

        public void ToggleSoundSettings()
        {
            mainScreen.SetActive(!mainScreen.activeSelf);
            soundSettingsScreen.SetActive(!soundSettingsScreen.activeSelf);
        }

        public void SetDesktopMode()
        {
            _xrMode = XrMode.Desktop;
            ModeButtonsColorTint();
        }

        public void SetVRMode()
        {
            _xrMode = XrMode.Vr;
            ModeButtonsColorTint();
        }

        public void ToggleVSync()
        {
            QualitySettings.vSyncCount = QualitySettings.vSyncCount switch
            {
                1 => 0,
                0 => 1,
                _ => QualitySettings.vSyncCount
            };

            Debug.Log(QualitySettings.vSyncCount);
        }    
        #endregion

        private void ModeButtonsColorTint()
        {
            switch (_xrMode)
            {
                case XrMode.Desktop:
                    desktopButtonModeColor.color = _pickedModeColor;
                    vrButtonModeColor.color = _defaultColor;
                    break;

                case XrMode.Vr:
                    desktopButtonModeColor.color = _defaultColor;
                    vrButtonModeColor.color = _pickedModeColor;
                    break;

            }
        }
    }
}