using UnityEngine;

namespace UnityDevKit.Validations
{
    public abstract class Validation<T> : MonoBehaviour, IValidation<T>
    {
        public abstract bool IsValid(T obj);
    }
}