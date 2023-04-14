using UnityEngine;
using UnityDevKit.XR;

namespace UnityDevKit.InteractionSystem.XR
{
    public class XrObjectController : MonoBehaviour
    {
        [SerializeField] private GameObject desktopObject;
        [SerializeField] private GameObject vrObject;

        private void Awake()
        {
            Handle();
        }

        public void Handle()
        {
            var isDesktop = XrChanger.XrMode == XrMode.Desktop;
            desktopObject.SetActive(isDesktop);
            vrObject.SetActive(!isDesktop);
        }
    }
}