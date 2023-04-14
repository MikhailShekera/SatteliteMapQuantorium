using System;
using System.Collections;
using UnityDevKit.Interactables;
using UnityEngine;

namespace UnityDevKit.Interactable.Grabbable
{
    [Obsolete("Class is obsolete", false)]
    public class GrabbableObject : GrabbableObjectBase
    {
        public override IEnumerator GrabProcess(GrabbableInteraction.GrabHolder grabHolder, GrabbableTempData lerpData)
        {
            while (Vector3.Distance(lerpData.grabbingObjectTransform.position, lerpData.holderTransform.position) > EPSILON ||
                   Vector3.Distance(lerpData.grabbingObjectTransform.eulerAngles, lerpData.holderTransform.eulerAngles) > EPSILON)
            {
                lerpData.grabbingObjectTransform.position = Vector3.Lerp(
                    lerpData.grabbingObjectTransform.position,
                    lerpData.holderTransform.position,
                    lerpData.positionTransitionSpeed * Time.deltaTime);

                lerpData.grabbingObjectTransform.eulerAngles = Vector3.Lerp(
                    lerpData.grabbingObjectTransform.eulerAngles,
                    lerpData.holderTransform.eulerAngles,
                    lerpData.rotationTransitionSpeed * Time.deltaTime);

                lerpData.positionTransitionSpeed *= ACCELERATION_MODIFIER;
                lerpData.rotationTransitionSpeed *= ACCELERATION_MODIFIER;
                yield return new WaitForEndOfFrame();
            }

            lerpData.grabbingObjectTransform.SetParent(lerpData.holderTransform);
            lerpData.grabbingObjectTransform.position = lerpData.holderTransform.position;
            lerpData.grabbingObjectTransform.eulerAngles = lerpData.holderTransform.eulerAngles;

            grabHolder.CurrentObject.ResetCollisions();
            grabHolder.IsGrabed = true;
        }
    }
}
