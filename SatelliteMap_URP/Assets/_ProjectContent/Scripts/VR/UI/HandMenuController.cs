using MyBox;
using UnityDevKit.Utils.SceneLoader;
using UnityEngine;
using Zenject;

namespace SatelliteMap.VR.UI
{
    public class HandMenuController : MonoBehaviour
    {
        [Separator("UI Part")] 
        [SerializeField] private GameObject exitScreen;

        private ISceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void ConfirmExit()
        {
            _sceneLoader.Quit();
        }

        public void DenyExit()
        {
            exitScreen.SetActive(false);
        }
    }
}