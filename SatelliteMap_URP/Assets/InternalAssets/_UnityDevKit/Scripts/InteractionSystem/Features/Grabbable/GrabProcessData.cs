using UnityEngine;

namespace UnityDevKit.InteractionSystem.Features.Grabbable
{
    public struct GrabProcessData
    {
        public Transform GrabbingObjectTransform;
        public Transform HolderTransform;
        public float PositionTransitionSpeed;
        public float RotationTransitionSpeed;
    }
}