using UnityEngine;
using UnityEngine.XR.Management;

namespace UnityDevKit.XR
{
    public static class XrChanger
    {
        public static XrMode XrMode { get; private set; } = XrMode.Desktop;
        
        public static void Change(bool value)
        {
            if (value)
            {
                ChangeToVR();
            }
            else
            {
                ChangeToDesktop();
            }
        }
        
        public static void Change(XrMode mode)
        {
            if (mode == XrMode) return;
            switch (mode)
            {
                case XrMode.Desktop:
                    ChangeToDesktop();
                    break;
                case XrMode.Vr:
                    ChangeToVR();
                    break;
            }
        }
        
        public static void ChangeToVR()
        {
            XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            XrMode = XrMode.Vr;
            Debug.Log("START XR");
        }

        public static void ChangeToDesktop()
        {
            if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                XRGeneralSettings.Instance.Manager.StopSubsystems();
            }
            XrMode = XrMode.Desktop;
            Debug.Log("STOP XR");
        }
    }
}