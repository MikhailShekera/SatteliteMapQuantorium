using UnityEngine;

namespace SatelliteMap.Data
{
    [CreateAssetMenu(fileName = "ZonePhotosScriptableObject", menuName = "ScriptableObject/ZonePhotosArray")]
    public class ZonePhotos : ScriptableObject
    {
        public string zoneName;
        public ImagesAndName[] images;
    }
}
