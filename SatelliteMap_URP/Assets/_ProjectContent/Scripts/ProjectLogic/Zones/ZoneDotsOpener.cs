using System.Collections.Generic;
using SatelliteMap.Data;
using SatelliteMap.WorldUI;
using UnityEngine;

namespace SatelliteMap.Zones
{
    public class ZoneDotsOpener : MonoBehaviour
    {
        [SerializeField] private MultipleWindowController windowController;
        [SerializeField] private List<EventAndImages> eventsAndImages;

        private void Awake()
        {
            foreach (var singleEvent in eventsAndImages)
            {
                singleEvent.interactableEvents.OnInteract.AddListener(_ =>
                    windowController.FillUIScreen(singleEvent.zonePhotos));
            }
        }
    }
}