using UnityEngine;
using UnityEngine.SceneManagement;

namespace SatelliteMap.Utils
{
    public class MainMenuUnloader : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
        }
    }
}