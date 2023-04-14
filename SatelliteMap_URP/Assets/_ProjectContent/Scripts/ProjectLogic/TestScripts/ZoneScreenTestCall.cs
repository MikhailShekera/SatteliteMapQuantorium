using SatelliteMap.Data;
using SatelliteMap.WorldUI;
using UnityEngine;

namespace SatelliteMap.Tests
{
    public class ZoneScreenTestCall : MonoBehaviour
    {
        [SerializeField] private MultipleWindowController modeController;
        [SerializeField] private ZonePhotos zonePhotos;

        public void Click()
        {
            modeController.FillUIScreen(zonePhotos);
        }
    }
}