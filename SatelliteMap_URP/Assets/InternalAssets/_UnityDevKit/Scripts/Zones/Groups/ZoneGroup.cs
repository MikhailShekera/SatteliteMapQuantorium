using System.Collections.Generic;
using System.Linq;
using UnityDevKit.Zones.Zones;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Zones.Groups
{
    public class ZoneGroup<T> : MonoBehaviour
        where T : Zone
    {
        [SerializeField] [InitializationField] private T[] zones;

        private T[] _workingZones;
        private bool _currentIsActive;
        protected T _currentZone;

        public IEnumerable<Zone> WorkingZones => _workingZones;
        
        private void Awake()
        {
            CacheZones();
        }

        private void Start()
        {
            AddListeners();
        }

        private void CacheZones()
        {
            if (zones.IsNullOrEmpty())
            {
                zones = GetComponentsInChildren<T>().ToArray();
            }
            if (zones.IsNullOrEmpty())
            {
                throw new ZoneException();
            }

            _workingZones = zones;
        }

        private void AddListeners()
        {
            foreach (var zone in zones)
            {
                zone.OnZoneEnter.AddListener(() => _currentZone = zone);
                zone.OnZoneExit.AddListener(() => _currentZone = _currentZone == zone ? null : _currentZone);
            }
        }

        private void ToggleToValue(bool isActive)
        {
            foreach (var zone in _workingZones)
            {
                zone.RootObject.SetActive(isActive);
            }

            _currentIsActive = isActive;
        }

        public T GetClosetZone(Vector3 selfPosition)
        {
            if (_workingZones.IsNullOrEmpty())
            {
                throw new ZoneException();
            }
            var closestZone = _workingZones[0];
            var minDistance = float.MaxValue;
            foreach (var zone in _workingZones)
            {
                var currentZoneDistance = Vector3.Distance(zone.transform.position, selfPosition);
                if (currentZoneDistance < minDistance)
                {
                    closestZone = zone;
                    minDistance = currentZoneDistance;
                }
            }

            return closestZone;
        }

        public void TurnOn()
        {
            ToggleToValue(true);
        }

        public void TurnOff()
        {
            ToggleToValue(false);
        }

        public void Toggle()
        {
            ToggleToValue(!_currentIsActive);
        }

        public void ToggleZonesByIndexes(IEnumerable<int> indexes)
        {
            _workingZones = zones.Where((_, i) => indexes.Contains(i)).ToArray();
        }
    }
}