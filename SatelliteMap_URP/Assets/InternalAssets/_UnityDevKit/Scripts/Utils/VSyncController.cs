using UnityEngine;

namespace UnityDevKit.Utils
{
    public static class VSyncController
    {
        public static void SetVSync()
        {
            SetVSyncValue(1);
        }
        
        public static void RemoveVSync()
        {
            SetVSyncValue(0);
        }
        
        public static void SetVSyncValue(int value)
        {
            QualitySettings.vSyncCount = value;
        }
    }
}