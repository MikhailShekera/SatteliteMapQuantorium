using DI.UI.Interactive.Holders;
using MyBox;
using System.Collections.Generic;
using SatelliteMap.Data;
using UnityEngine;
using Zenject;

namespace SatelliteMap.WorldUI
{
    public class MultipleWindowController : MonoBehaviour
    {
        [Separator("Prefabs To Instantiate")] 
        [SerializeField] private GameObject windowPrefab;

        [Separator("Amount Of Opened Windows")] 
        [SerializeField, PositiveValueOnly, InitializationField] private List<GameObject> windowSpawnCoordinates;

        private IInteractiveHolder _interactiveHolder;
        private OpenedWindowsHolder _openedWindowsHolder;

        private int _currentAmountOfWindows;
        private int _lastCreatedIndex = 0;
        private bool _windowsCoordinatesMaxLock;
        private bool _replaceFlag;

        private Transform _transform;

        private struct WindowData
        {
            public GameObject Window;
            public SingleWindowController WindowController;
        }

        private void Awake()
        {
            _transform = transform;

            _openedWindowsHolder = new OpenedWindowsHolder();
            _openedWindowsHolder.SetArraySize(windowSpawnCoordinates.Count);
        }

        [Inject]
        private void Construct(IInteractiveHolder interactiveHolder)
        {
            _interactiveHolder = interactiveHolder;
        }

        public void FillUIScreen(ZonePhotos data)
        {
            var newPosition = GetPositionDelta();
            var newWindow = Instantiate(windowPrefab, newPosition, Quaternion.identity, _transform);

            var newWindowController = newWindow.GetComponentInChildren<SingleWindowController>();

            _interactiveHolder.AddCanvas(newWindow);
            newWindowController.OnWindowClose.AddListener(() =>
                _interactiveHolder.RemoveCanvas(newWindow)); // TODO -- Change to cached remove

            newWindowController.SetUpImagesAndButtons(data);

            if (_currentAmountOfWindows < windowSpawnCoordinates.Count)
            {
                _openedWindowsHolder.SetWindowAndController(_currentAmountOfWindows, newWindow, newWindowController);

                if (!_windowsCoordinatesMaxLock)
                {
                    _openedWindowsHolder.SetPositionByIndex(_currentAmountOfWindows);
                }

                _lastCreatedIndex++;
                _currentAmountOfWindows++;
            }
            else
            {
                _windowsCoordinatesMaxLock = true;
                _replaceFlag = _openedWindowsHolder.RewriteWindow(windowSpawnCoordinates.Count, newWindow);

                if (!_replaceFlag)
                {
                    _lastCreatedIndex++;

                    if (_lastCreatedIndex > windowSpawnCoordinates.Count - 1)
                    {
                        _lastCreatedIndex = 0;
                    }

                    _interactiveHolder.RemoveCanvas(_openedWindowsHolder.OpenedWindowsData[_lastCreatedIndex].Window);
                    _openedWindowsHolder.DestroyOldestWindow(_lastCreatedIndex, newWindow);
                }
            }

            newWindow.transform.rotation =
                Quaternion.LookRotation(-Camera.main.transform.position + newWindow.transform.position);
        }

        private Vector3 GetPositionDelta()
        {
            try
            {
                return windowSpawnCoordinates[_lastCreatedIndex].transform.position;
            }
            catch
            {
                return windowSpawnCoordinates[0].transform.position;
            }
        }
    }
}