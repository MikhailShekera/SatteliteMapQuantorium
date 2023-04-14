using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityDevKit.Validations
{
    public abstract class TransformValidation : Validation<Transform>
    {
        public static bool Validate(IEnumerable<TransformValidation> validations, Transform t) =>
            validations.All(validation => validation.IsValid(t));
    }
}