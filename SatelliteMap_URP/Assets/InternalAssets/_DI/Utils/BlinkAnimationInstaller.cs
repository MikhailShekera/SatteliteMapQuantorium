using UnityDevkit.Animation;
using UnityEngine;
using Zenject;

namespace DI.Utils
{
    public class BlinkAnimationInstaller : MonoInstaller
    {
        [SerializeField] private BlinkAnimation blinkAnimation;
        
        public override void InstallBindings()
        {
            Container
                .Bind<BlinkAnimation>()
                .FromInstance(blinkAnimation)
                .AsSingle()
                .NonLazy();
        }
    }
}