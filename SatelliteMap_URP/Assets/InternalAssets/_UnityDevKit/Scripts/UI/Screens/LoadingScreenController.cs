using UnityDevKit.XR;
using UnityEngine;

namespace UnityDevKit.UI.Screens
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject vrControls;
        [SerializeField] private GameObject desktopControls;

        public void SetLoadingScreen(XrMode mode)
        {
            switch (mode)
            {
                case XrMode.Desktop:
                    vrControls.SetActive(false);
                    desktopControls.SetActive(true);
                    break;
                case XrMode.Vr:
                    vrControls.SetActive(true);
                    desktopControls.SetActive(false);
                    break;
            }
        }
    }
}