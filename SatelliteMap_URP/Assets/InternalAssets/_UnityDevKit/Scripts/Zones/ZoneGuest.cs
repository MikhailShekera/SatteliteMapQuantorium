using System.Collections;
using UnityDevKit.Zones.Zones;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Zones.Guests
{
    [RequireComponent(typeof(Collider))]
    public abstract class ZoneGuest<TZone> : BaseZoneGuest
    where TZone : Zone
    {
        public UnityEvent<TZone> onZoneEnter;
        public UnityEvent<TZone> onZoneExit;

        private Collider _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!CheckZone(other, out var zone)) return;
            IsInZone = true;
            HandleOnZoneEnter(zone);
            zone.EnterZone();
            onZoneEnter.Invoke(zone);
            StartCoroutine(Refresh(zone, other));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!CheckZone(other, out var zone)) return;
            IsInZone = false;
            HandleOnZoneExit(zone);
            zone.ExitZone();
            onZoneExit.Invoke(zone);
            StopAllCoroutines();
        }

        private IEnumerator Refresh(TZone zone, Collider zoneCollider)
        {
            const float refreshPeriod = 0.5f;
            yield return new WaitForSeconds(refreshPeriod);
            while (IsInZone)
            {
                var currentIsInZone = _collider.bounds.Intersects(zoneCollider.bounds);
                if (currentIsInZone || !zone.IsActiveRoot)
                {
                    yield return new WaitForSeconds(refreshPeriod);
                }
                else
                {
                    IsInZone = false;
                    HandleOnZoneExit(zone);
                    zone.ExitZone();
                    onZoneExit.Invoke(zone);
                    break;
                }
            }
        }

        private static bool CheckZone(Collider other, out TZone zone)
        {
            zone = other.GetComponent<TZone>();
            return zone != null;
        }

        protected virtual void HandleOnZoneEnter(TZone zone)
        {
        }

        protected virtual void HandleOnZoneExit(TZone zone)
        {
        }
    }
}