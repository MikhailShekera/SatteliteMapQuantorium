using SatelliteMap.Data;
using SatelliteMap.WorldUI;
using UnityDevKit.Events;
using UnityEngine;
using Zenject;

namespace SatelliteMap.Zones
{
    public class ZoneDotOpener : MonoBehaviour
    {
        [SerializeField] private EventFlow triggerEvent;
        [SerializeField] private ZonePhotos zonePhotos;

        private MultipleWindowController _windowController;

        [Inject]
        private void Construct(MultipleWindowController windowController)
        {
            _windowController = windowController;
        }
        
        private void Awake()
        {
            if (zonePhotos)
            {
                triggerEvent.AddListener(() => _windowController.FillUIScreen(zonePhotos));
            }
            else
            {
                Debug.LogWarning("[ZoneDotOpener] You have to setup zonePhotos");
            }
        }
    }
}