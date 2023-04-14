using System.Collections;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Features.Grabbable
{
    public class GrabbableObject : BaseGrabbableObject
    {
        protected override IEnumerator GrabProcess(GrabProcessData lerpData)
        {
            while (Vector3.Distance(lerpData.GrabbingObjectTransform.position, lerpData.HolderTransform.position) > EPSILON ||
                   Vector3.Distance(lerpData.GrabbingObjectTransform.eulerAngles, lerpData.HolderTransform.eulerAngles) > EPSILON)
            {
                lerpData.GrabbingObjectTransform.position = Vector3.Lerp(
                    lerpData.GrabbingObjectTransform.position,
                    lerpData.HolderTransform.position,
                    lerpData.PositionTransitionSpeed * Time.deltaTime);

                lerpData.GrabbingObjectTransform.eulerAngles = Vector3.Lerp(
                    lerpData.GrabbingObjectTransform.eulerAngles,
                    lerpData.HolderTransform.eulerAngles,
                    lerpData.RotationTransitionSpeed * Time.deltaTime);

                lerpData.PositionTransitionSpeed *= ACCELERATION_MODIFIER;
                lerpData.RotationTransitionSpeed *= ACCELERATION_MODIFIER;
                yield return new WaitForEndOfFrame();
            }

            lerpData.GrabbingObjectTransform.SetParent(lerpData.HolderTransform);
            lerpData.GrabbingObjectTransform.position = lerpData.HolderTransform.position;
            lerpData.GrabbingObjectTransform.eulerAngles = lerpData.HolderTransform.eulerAngles;

            ResetCollisions();
        }
    }
}