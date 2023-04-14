using UnityDevKit.InputSystem;

namespace UnityDevKit.Player.Extensions
{
    public abstract class BindedPlayerExtension : PlayerExtension, IInputControlsBinder
    {
        protected override void Start()
        {
            base.Start();
            AddBindings();
        }

        protected override void PlayerLoop()
        {
            base.PlayerLoop();
        }

        public abstract void AddBindings();
    }
}