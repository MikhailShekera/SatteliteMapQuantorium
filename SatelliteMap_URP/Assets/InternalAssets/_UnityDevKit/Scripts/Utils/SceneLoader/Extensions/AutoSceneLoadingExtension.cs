using System.Collections;
using MyBox;
using UnityEngine;
using Zenject;

namespace UnityDevKit.Utils.SceneLoader
{
    public abstract class AutoSceneLoadingExtension : MonoBehaviour, ICoroutineExtension
    {
        [Separator("Auto extension settings")]
        [SerializeField] private SceneLoadMethod loadMethod = SceneLoadMethod.SceneLoad;
        [SerializeField] private SceneLoadingExtensionType loadingType;
        
        [Inject]
        private void Construct(IExtensibleSceneLoader sceneLoader)
        {
            sceneLoader.AddLoadingExtension(this, loadMethod, loadingType);
        }
        
        public abstract IEnumerator Execute();
    }
}