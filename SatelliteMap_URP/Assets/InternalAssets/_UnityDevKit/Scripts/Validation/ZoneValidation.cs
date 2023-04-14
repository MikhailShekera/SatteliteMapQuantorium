using UnityDevKit.Zones.Guests;
using UnityEngine;

namespace UnityDevKit.Validations
{
    public class ZoneValidation : TransformValidation
    {
        [SerializeField] private BaseZoneGuest zoneGuest;

        public override bool IsValid(Transform obj) => zoneGuest.IsInZone;
    }
}