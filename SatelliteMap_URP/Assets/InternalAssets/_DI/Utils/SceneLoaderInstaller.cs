using UnityDevKit.Utils.SceneLoader;
using UnityEngine;
using Zenject;

namespace DI.Utils
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader sceneLoader;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ISceneLoader>()
                .FromInstance(sceneLoader)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<IExtensibleSceneLoader>()
                .FromInstance(sceneLoader)
                .AsSingle()
                .NonLazy();
        }
    }
}