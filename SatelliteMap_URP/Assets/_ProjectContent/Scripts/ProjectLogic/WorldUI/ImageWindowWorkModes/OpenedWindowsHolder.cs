using UnityEngine;

namespace SatelliteMap.WorldUI
{
    public class OpenedWindowsHolder : MonoBehaviour
    {
        private OpenedWindowsData[] _openedWindowsData;

        public OpenedWindowsData[] OpenedWindowsData => _openedWindowsData;

        public void SetArraySize(int size)
        {
            _openedWindowsData = new OpenedWindowsData[size];
        }

        public void SetWindowAndController(int index, GameObject window, SingleWindowController windowController)
        {
            _openedWindowsData[index].Window = window;
            _openedWindowsData[index].WindowController = windowController;
        }

        public void SetPositionByIndex(int index)
        {
            _openedWindowsData[index].WindowCoordinates = _openedWindowsData[index].Window.transform.position;
        }

        public bool RewriteWindow(int maximumWindowsAmount, GameObject newWindow)
        {
            for (var i = 0; i < maximumWindowsAmount; i++)
            {
                if (_openedWindowsData[i].Window.activeSelf == false)
                {
                    Destroy(_openedWindowsData[i].Window.gameObject);
                    _openedWindowsData[i].Window = newWindow;
                    _openedWindowsData[i].Window.transform.position = _openedWindowsData[i].WindowCoordinates;

                    return true;
                }
            }

            return false;
        }

        public void DestroyOldestWindow(int index, GameObject newWindow)
        {
            Destroy(_openedWindowsData[index].Window.gameObject);

            _openedWindowsData[index].Window = newWindow;
            _openedWindowsData[index].Window.transform.position = _openedWindowsData[index].WindowCoordinates;
        }
    }
}