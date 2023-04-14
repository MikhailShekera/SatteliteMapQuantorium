namespace UnityDevKit.Effects.Highlight.Loaders
{
    public interface IPropertiesLoader<out TProperties>
    {
        TProperties GetProperties();
    }
}