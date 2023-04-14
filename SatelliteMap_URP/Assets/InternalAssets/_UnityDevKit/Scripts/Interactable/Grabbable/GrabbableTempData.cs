using System;
using UnityEngine;

namespace UnityDevKit.Interactable.Grabbable
{
    [Obsolete("Struct is obsolete", false)]
    public struct GrabbableTempData
    {
        public Transform grabbingObjectTransform;
        public Transform holderTransform;
        public float positionTransitionSpeed;
        public float rotationTransitionSpeed;
    }
}