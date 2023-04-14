using UnityEngine;
using Zenject;

namespace UnityDevKit.Utils.SceneLoader
{
    public class SceneReloader : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _sceneLoader.RestartScene();
            }
        }
    }
}
