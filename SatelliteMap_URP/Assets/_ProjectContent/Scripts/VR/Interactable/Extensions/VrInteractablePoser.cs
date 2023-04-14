using System;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.HandPoser;
using MyBox;
using UnityEngine;

namespace SatelliteMap.VR.Interactable.Extensions
{
    public class VrInteractablePoser : VrInteractableExtension
    {
        [SerializeField] private PoserData focusPoser;
        [SerializeField] private PoserData interactPoser;
        [SerializeField] private PoserData afterInteractPoser;

        [Serializable]
        private class PoserData
        {
            [SerializeField] private bool usePoser;

            [SerializeField, ConditionalField(nameof(usePoser))] private HVRHandPoser poser;

            public void SetPose(HVRHandGrabber handGrabber)
            {
                if (usePoser)
                {
                    handGrabber.SetAnimatorOverridePose(poser);
                }
            }

            public void ResetPose(HVRHandGrabber handGrabber)
            {
                handGrabber.SetAnimatorOverridePose(null);
            }
        }

        protected override void OnFocusAction(HVRHandGrabber handGrabber)
        {
            base.OnFocusAction(handGrabber);
            focusPoser.SetPose(handGrabber);
        }

        protected override void OnDeFocusAction(HVRHandGrabber handGrabber)
        {
            base.OnDeFocusAction(handGrabber);
            focusPoser.ResetPose(handGrabber);
        }

        protected override void OnInteractAction(HVRHandGrabber handGrabber)
        {
            base.OnInteractAction(handGrabber);
            interactPoser.SetPose(handGrabber);
        }

        protected override void OnAfterInteractAction(HVRHandGrabber handGrabber)
        {
            base.OnAfterInteractAction(handGrabber);
            afterInteractPoser.SetPose(handGrabber);
        }
    }
}