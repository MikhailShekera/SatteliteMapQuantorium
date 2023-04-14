using DI.UI.Interactive.Holders;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;
using Zenject;

namespace SatelliteMap.VR.UI
{
    public class VrExitMenu : MonoBehaviour
    {
        [SerializeField] private HVRHandGrabber hand;
        [SerializeField] private GameObject menu;
        
        private IInteractiveHolder _interactiveHolder;
        
        [Inject]
        private void Construct(IInteractiveHolder interactiveHolder)
        {
            _interactiveHolder = interactiveHolder;
        }
        
        private void Awake()
        {
            if (!hand)
            {
                if (!TryGetComponent(out hand))
                {
                    Debug.Log("[VrExitMenu] Has no hand reference");
                }
            }
        }

        private void Start()
        {
            _interactiveHolder ??= FindObjectOfType<VrInteractiveUiHolder>(); // TODO -- DI
            _interactiveHolder.AddCanvas(menu);
            menu.SetActive(false);
        }

        private void Update()
        {
            if (hand.Controller.JoystickButtonState.JustActivated)
            {
                ToggleMenu();
            }
        }

        private void ToggleMenu()
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}