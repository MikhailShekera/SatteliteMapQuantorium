using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Core.Grabbers;
using MyBox;
using UnityDevKit.InteractionSystem.Features.Rotation;
using UnityEngine;

namespace VrExtensions.InteractionSystem.Features.Rotation
{
    public class VrObjectRotation : MonoBehaviour
    {
        [Separator("VR")] [SerializeField, InitializationField]
        private HVRHandGrabber[] hands;

        [Separator("Interactable")] [SerializeField, InitializationField]
        private Transform interactable;

        [SerializeField, PositiveValueOnly, InitializationField]
        private float sens = 1;

        [SerializeField, PositiveValueOnly, InitializationField]
        private float interactDistance = 2f;

        private Dictionary<HVRHandGrabber, RotationEngine> _rotationEngines =
            new Dictionary<HVRHandGrabber, RotationEngine>();

        private int _focusedHandsCounter;
        
        private void Start()
        {
            _rotationEngines = hands.Select(
                    hand => new KeyValuePair<HVRHandGrabber, RotationEngine>(
                        hand,
                        new RotationEngine(
                            interactable,
                            interactDistance,
                            sens)))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private void Update()
        {
            if (_focusedHandsCounter == 0) return;
            foreach (var (hand, engine) in _rotationEngines)
            {
                engine.Update(hand.Controller.GripButtonState.Active, hand.Palm);
            }
        }

        public void AddFocusedHand()
        {
            _focusedHandsCounter++;
        }

        public void RemoveFocusedHand()
        {
            _focusedHandsCounter--;
        }
    }
}