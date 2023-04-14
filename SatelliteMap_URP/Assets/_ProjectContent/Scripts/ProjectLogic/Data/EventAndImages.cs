using System;
using UnityDevKit.Interactables;

namespace SatelliteMap.Data
{
    [Serializable]
    [Obsolete("Obsolete")]
    public struct EventAndImages
    {
        public ZonePhotos zonePhotos;
        public InteractableBase interactableEvents;
    }
}