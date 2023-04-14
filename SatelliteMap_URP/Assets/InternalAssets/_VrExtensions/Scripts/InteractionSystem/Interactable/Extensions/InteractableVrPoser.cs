using System;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.HandPoser;
using MyBox;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityEngine;

namespace VrExtensions.InteractionSystem.Interactable.Extensions
{
    public class InteractableVrPoser : InteractableXrExtension
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
        
        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            var handGrabber = ArgsProcessor.Process(args).HandGrabber;
            focusPoser.SetPose(handGrabber);
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            var handGrabber = ArgsProcessor.Process(args).HandGrabber;
            focusPoser.ResetPose(handGrabber);
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            var handGrabber = ArgsProcessor.Process(args).HandGrabber;
            interactPoser.SetPose(handGrabber);
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            var handGrabber = ArgsProcessor.Process(args).HandGrabber;
            afterInteractPoser.SetPose(handGrabber);
        }
    }
}