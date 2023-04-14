using UnityEngine;

namespace UnityDevKit.Zones.Zones.Extensions
{
    [RequireComponent(typeof(Zone))]
    public abstract class ZoneExtension : MonoBehaviour
    {
        private Zone _zone;

        private void Awake()
        {
            _zone = GetComponent<Zone>();
        }

        private void Start()
        {
            _zone.OnZoneEnter.AddListener(OnZoneEnter);
            _zone.OnZoneExit.AddListener(OnZoneExit);
        }

        protected abstract void OnZoneEnter();
        protected abstract void OnZoneExit();
    }
}