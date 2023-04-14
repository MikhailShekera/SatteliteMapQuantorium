namespace UnityDevKit.Validations
{
    public interface IValidation<in T>
    {
        bool IsValid(T obj);
    }
}