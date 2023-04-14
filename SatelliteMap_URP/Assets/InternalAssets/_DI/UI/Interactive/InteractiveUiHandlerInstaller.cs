using DI.UI.Interactive.Holders;
using UnityDevKit.XR;
using UnityEngine;
using Zenject;

namespace DI.UI.Interactive
{
    public class InteractiveUiHandlerInstaller : MonoInstaller
    {
        [SerializeField] private InteractiveHolder desktopInteractiveUiHandler;
        [SerializeField] private InteractiveHolder vrInteractiveUiHandler;

        public override void InstallBindings()
        {
            var interactiveUiHandler =
                XrChanger.XrMode == XrMode.Desktop
                    ? desktopInteractiveUiHandler
                    : vrInteractiveUiHandler;
            Container
                .Bind<IInteractiveHolder>()
                .FromInstance(interactiveUiHandler)
                .AsSingle()
                .NonLazy();
        }
    }
}