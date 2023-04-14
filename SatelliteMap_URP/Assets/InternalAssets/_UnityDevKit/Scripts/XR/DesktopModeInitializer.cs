namespace UnityDevKit.XR
{
    public class DesktopModeInitializer : XRModeInitializer
    {
        public override XrMode GetMode() => XrMode.Desktop;

        protected override void InitializeXRMode()
        {
            XrChanger.ChangeToDesktop();
        }
    }
}