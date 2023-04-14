using UnityDevKit.XR;

namespace UnityDevKit.Utils.SceneLoader
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName, XrMode xrMode);
        void LoadScene(string sceneName);
        void RestartScene();
        void Quit();
    }
}