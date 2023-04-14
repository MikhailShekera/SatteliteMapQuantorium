namespace UnityDevKit.Validations
{
    public static class ValidationMethods
    {
        public static bool Validate<T>(this IValidation<T>[] validations, T validationObject)
        {
            for (var i = 0; i < validations.Length; i++)
            {
                if (!validations[i].IsValid(validationObject))
                {
                    return false;
                }
            }

            return true;
        }
    }
}