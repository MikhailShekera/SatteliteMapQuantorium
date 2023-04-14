namespace UnityDevKit.Utils.SceneLoader
{
    public interface IExtensibleSceneLoader
    {
        void AddLoadingExtension(
            ICoroutineExtension extension,
            SceneLoadMethod sceneLoadMethod,
            SceneLoadingExtensionType extensionType);
    }
}