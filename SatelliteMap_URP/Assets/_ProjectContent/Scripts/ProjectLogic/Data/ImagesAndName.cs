using System;
using UnityEngine;

namespace SatelliteMap.Data
{
    [Serializable]
    public struct ImagesAndName
    {
        public Sprite image;
        public Sprite processedImage;
        public string spriteName;
    }
}