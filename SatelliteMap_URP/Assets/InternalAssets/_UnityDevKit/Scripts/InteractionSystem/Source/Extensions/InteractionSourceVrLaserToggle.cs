using UnityDevKit.InteractionSystem.Core.Args;

namespace UnityDevKit.InteractionSystem.Source.Extensions
{
    public class InteractionSourceVrLaserToggle : InteractionSourceExtension
    {
        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            var laser = args.InteractionSource.GetComponentInChildren<Laser.Laser>();
            if (laser)
            {
                laser.Show();
            }
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            var laser = args.InteractionSource.GetComponentInChildren<Laser.Laser>();
            if (laser)
            {
                laser.Hide();
            }
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            var laser = args.InteractionSource.GetComponentInChildren<Laser.Laser>();
            if (laser)
            {
                laser.Increase();
            }
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            var laser = args.InteractionSource.GetComponentInChildren<Laser.Laser>();
            if (laser)
            {
                laser.Decrease();
            }
        }
    }
}