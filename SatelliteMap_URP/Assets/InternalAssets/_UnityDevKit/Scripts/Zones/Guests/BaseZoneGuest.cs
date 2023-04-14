using UnityEngine;

namespace UnityDevKit.Zones.Guests
{
    public abstract class BaseZoneGuest : MonoBehaviour
    {
        public bool IsInZone { get; protected set; }
    }
}