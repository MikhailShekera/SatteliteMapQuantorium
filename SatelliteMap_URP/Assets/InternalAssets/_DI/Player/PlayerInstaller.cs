using UnityDevKit.Player.Controllers;
using UnityEngine;
using Zenject;

namespace DI.Utils
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController playerController;

        public override void InstallBindings()
        {
            Container
                .Bind<PlayerController>()
                .FromInstance(playerController)
                .AsSingle()
                .NonLazy();
        }
    }
}