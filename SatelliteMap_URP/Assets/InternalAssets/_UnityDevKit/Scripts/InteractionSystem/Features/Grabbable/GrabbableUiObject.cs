using System.Collections;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Features.Grabbable
{
    public class GrabbableUiObject : BaseGrabbableObject
    {
        protected override IEnumerator GrabProcess(GrabProcessData lerpData)
        {
            var distance = Vector3.Distance(lerpData.HolderTransform.position, grabPoint.position);

            lerpData.GrabbingObjectTransform.SetParent(lerpData.HolderTransform);
            lerpData.GrabbingObjectTransform.position = lerpData.HolderTransform.position;
            lerpData.GrabbingObjectTransform.localPosition += Vector3.forward * distance;
            lerpData.GrabbingObjectTransform.eulerAngles = lerpData.HolderTransform.eulerAngles;

            ResetCollisions();
            yield return null;
        }
    }
}