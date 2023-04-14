using DI.UI.Interactive.Holders;
using MyBox;
using SatelliteMap.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SatelliteMap.WorldUI
{
    public class SingleWindowModeController : WindowControllerBase
    {
        [Separator("Single Window Controller")]
        [SerializeField] private SingleWindowController singleWindowController;

        [Separator("Scrollbar")]
        [SerializeField] private Scrollbar scrollbar;

        private IInteractiveHolder _interactiveHolder;

        private void Start()
        {
            _interactiveHolder.AddCanvas(gameObject);
        }

        [Inject]
        private void Construct(IInteractiveHolder interactiveHolder)
        {
            _interactiveHolder = interactiveHolder;
        }

        public override void FillUIScreen(ZonePhotos data)
        {
            if (!singleWindowController.Canvas.activeSelf)
            {
                singleWindowController.Canvas.SetActive(true);
            }

            ClearList();

            if (singleWindowController.FullScreenImage.activeSelf)
            {
                singleWindowController.ToggleScreensInSingleWindow();
            }

            scrollbar.value = 1;

            singleWindowController.SetUpImagesAndButtons(data);
        }

        private void ClearList()
        {
            foreach (Transform child in singleWindowController.ContentWindow.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
