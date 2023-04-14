using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace SatelliteMap.VR.RayPointer
{
    public struct LaserPointerEventArgs
    {
        public HVRHandGrabber FromHand;
        public float Distance;
        public Transform Target;
    }
}