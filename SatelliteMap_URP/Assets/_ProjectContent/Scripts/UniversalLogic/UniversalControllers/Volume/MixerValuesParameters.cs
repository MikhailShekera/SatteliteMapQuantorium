using System;

namespace SatelliteMap.Scripts.UniversalLogic.UniversalControllers.Volume
{
    [Serializable]
    public class MixerValuesParameters<T>
    {
        public string name = "value";
        public T value;
    }
}