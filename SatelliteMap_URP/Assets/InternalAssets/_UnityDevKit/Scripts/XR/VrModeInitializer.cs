namespace UnityDevKit.XR
{
    public class VrModeInitializer : XRModeInitializer
    {
        public override XrMode GetMode() => XrMode.Vr;

        protected override void InitializeXRMode()
        {
            XrChanger.ChangeToVR();
        }
    }
}