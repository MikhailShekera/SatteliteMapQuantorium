using UnityDevKit.Utils.Objects;
using UnityEngine;

namespace UnityDevKit.Validations
{
    public class VisibilityValidation : TransformValidation
    {
        [SerializeField] private VisibilityValidator validator;
        [SerializeField] private Transform observer;

        public override bool IsValid(Transform obj)
        {
            var isVisible = validator.Validate(observer, obj);
            Debug.Log($"Is visible: {isVisible}");
            return isVisible;
        }
    }
}