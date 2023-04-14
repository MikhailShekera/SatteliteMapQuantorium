using System.Collections.Generic;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using ModestTree;
using UnityEngine;

namespace SatelliteMap.VR.Interactable
{
    [RequireComponent(typeof(Collider))]
    public class VrInteractableHandCollider : VrInteractableBase
    {
        [Tooltip("Decide to ignore non trigger colliders or not on the hand for stable detection of non moving colliders.")]
        public bool useHandTriggersOnly = false;
        
        [Tooltip("Layer mask of the hand collider for performant detection")]
        public LayerMask handMask;

        private readonly Dictionary<HVRHandGrabber, List<Collider>> _handsColliders = new Dictionary<HVRHandGrabber, List<Collider>>();

        private void Update()
        {
            foreach (var hand in _handsColliders.Keys)
            {
                if (hand.Controller.TriggerButtonState.JustActivated)
                {
                    Interact(hand);
                }
                else if (hand.Controller.TriggerButtonState.JustDeactivated)
                {
                    AfterInteract(hand);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // if ((handMask & other.transform.gameObject.layer) == 0)
            // {
            //     return;
            // }
            
            var hand = other.FindComponent<HVRHandGrabber>();
            if (!hand) return;

            // if (other.isTrigger)
            // {
            //     if (!other.TryGetComponent(out HVRHandTrigger _))
            //     {
            //         //ignore triggers that aren't marked with HVRHandTrigger or a child of the hand model so that other triggers on the hand rigidbody
            //         //that are used for other purposes like the distance grabber are aren't considered
            //         if (hand.HandAnimator && (!other.transform.IsChildOf(hand.HandAnimator.Hand.transform)) && other.transform != hand.HandAnimator.Hand.transform)
            //         {
            //             return;
            //         }
            //     }
            // }
            // else if (UseHandTriggersOnly)
            // {
            //     return;
            // }

            if (!_handsColliders.ContainsKey(hand))
            {
                _handsColliders.Add(hand, new List<Collider>(5));
                Focus(hand);
            }

            if (!_handsColliders[hand].Contains(other))
            {
                _handsColliders[hand].Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // if ((handMask & other.transform.gameObject.layer) == 0)
            // {
            //     return;
            // }
            
            var hand = other.FindComponent<HVRHandGrabber>();
            if (!hand) return;

            // if (other.isTrigger)
            // {
            //     if (!other.TryGetComponent(out HVRHandTrigger _))
            //     {
            //         //ignore triggers that aren't marked with HVRHandTrigger or a child of the hand model so that other triggers on the hand rigidbody
            //         //that are used for other purposes like the distance grabber are aren't considered
            //         if (hand.HandAnimator && (!other.transform.IsChildOf(hand.HandAnimator.Hand.transform)) && other.transform != hand.HandAnimator.Hand.transform)
            //         {
            //             return;
            //         }
            //     }
            // }
            // else if (UseHandTriggersOnly)
            // {
            //     return;
            // }
            
            if (!_handsColliders.ContainsKey(hand))
            {
                return;
            }
            
            if (_handsColliders[hand].Contains(other))
            {
                _handsColliders[hand].Remove(other);
            }

            if (_handsColliders[hand].IsEmpty())
            {
                DeFocus(hand);
                _handsColliders.Remove(hand);
            }
        }
    }
}