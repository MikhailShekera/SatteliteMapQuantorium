using System;

namespace UnityDevKit.InputSystem.Manager
{
    [Serializable]
    public class UniversalMapToggleException : Exception
    {
        public UniversalMapToggleException() { }

        public UniversalMapToggleException(string message)
            : base(message) { }
    }
}
