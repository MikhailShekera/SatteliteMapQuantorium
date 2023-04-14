using SatelliteMap.WorldUI;
using UnityEngine;
using Zenject;

namespace SatelliteMap.DI.UI
{
    public class MultipleWindowControllerInstaller : MonoInstaller
    {
        [SerializeField] private MultipleWindowController multipleWindowController;

        public override void InstallBindings()
        {
            Container
                .Bind<MultipleWindowController>()
                .FromInstance(multipleWindowController)
                .AsSingle()
                .NonLazy();
        }
    }
}