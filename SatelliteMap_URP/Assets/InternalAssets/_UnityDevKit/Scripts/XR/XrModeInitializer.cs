using UnityDevKit.Patterns;
using UnityEngine;

namespace UnityDevKit.XR
{
    [DisallowMultipleComponent]
    public abstract class XRModeInitializer : Singleton<XRModeInitializer>
    {
        public override void Awake()
        {
            InitializeXRMode();
        }

        public abstract XrMode GetMode();
        protected abstract void InitializeXRMode();
    }
}