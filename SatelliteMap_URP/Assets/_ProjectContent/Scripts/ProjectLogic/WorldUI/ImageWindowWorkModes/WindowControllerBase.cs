using SatelliteMap.Data;
using UnityEngine;

namespace SatelliteMap.WorldUI
{
    public abstract class WindowControllerBase : MonoBehaviour
    {
        public abstract void FillUIScreen(ZonePhotos data);
    }
}