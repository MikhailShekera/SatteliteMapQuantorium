using System;
using System.Collections;
using UnityDevKit.Interactables;
using UnityEngine;

namespace UnityDevKit.Interactable.Grabbable
{
    [DisallowMultipleComponent] [Obsolete("Class is obsolete", false)]
    public class GrabbableUIObject : GrabbableObjectBase
    {
        public override IEnumerator GrabProcess(GrabbableInteraction.GrabHolder grabHolder, GrabbableTempData lerpData)
        {
            float distance = Vector3.Distance(lerpData.holderTransform.position, GrabPoint.position);

            lerpData.grabbingObjectTransform.SetParent(lerpData.holderTransform);
            lerpData.grabbingObjectTransform.position = lerpData.holderTransform.position;
            lerpData.grabbingObjectTransform.localPosition += Vector3.forward * distance;
            lerpData.grabbingObjectTransform.eulerAngles = lerpData.holderTransform.eulerAngles;

            grabHolder.CurrentObject.ResetCollisions();
            grabHolder.IsGrabed = true;
            yield return null;
        }
    }
}