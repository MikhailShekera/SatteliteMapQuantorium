using UnityDevKit.Scenarios;
using UnityEngine;
using Zenject;

namespace DI.Utils
{
    public class ScenarioInstaller : MonoInstaller
    {
        [SerializeField] private ScenarioController scenarioController;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ScenarioController>()
                .FromInstance(scenarioController)
                .AsSingle()
                .NonLazy();
        }
    }
}