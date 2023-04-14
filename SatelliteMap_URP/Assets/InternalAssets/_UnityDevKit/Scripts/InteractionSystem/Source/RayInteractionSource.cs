using MyBox;
using UnityDevKit.Events;
using UnityDevKit.InteractionSystem.Core.ActionsInput;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Source
{
    public class RayInteractionSource : BaseInteractionSource
    {
        [Separator("Raycast settings")] 
        [SerializeField] private Transform origin;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float maxRayDistance = 10f;

        [Separator("Additional settings")] 
        [SerializeField] private SourceHolder sourceHolder;
        [SerializeField, Range(1, 120)] private int frameCountFilter = 1;

        [Separator("Actions input")] 
        [SerializeField] private InteractionActionInput interactInput;
        [SerializeField] private InteractionActionInput interactHoldInput;
        [SerializeField] private InteractionActionInput afterInteractInput;

        public readonly EventHolder<RaycastBaseData> OnRaycast = new EventHolder<RaycastBaseData>();

        public Transform Origin => origin;

        public struct RaycastBaseData
        {
            public bool HasHit;
            public float Distance;
        }

        private Transform _previousContact;
        private float _currentDistance;

        private Transform SourceHolder => sourceHolder.Holder;

        private void Update()
        {
            if (!IsActivated || Time.frameCount % frameCountFilter != 0) return;

            var hasHit = Physics.Raycast( // TODO -- multiple hit
                origin.position,
                origin.forward,
                out var hit,
                maxRayDistance,
                layerMask);

            _currentDistance = hasHit ? hit.distance : maxRayDistance;
            Debug.DrawRay(origin.position, origin.forward * _currentDistance, Color.magenta);

            OnRaycast.Invoke(new RaycastBaseData { HasHit = hasHit, Distance = _currentDistance });
            ProcessPointer(hasHit, hit);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            var args = new InteractionArgs
            {
                InteractionSource = source,
                SourceHolder = SourceHolder,
                Distance = 0f,
                Target = _previousContact
            };
            OnRaycast.Invoke(new RaycastBaseData { HasHit = false, Distance = 0 });
            AfterInteract(args);
            DeFocus(args);
            _previousContact = null;
        }

        private void ProcessPointer(bool hasHit, RaycastHit hit)
        {
            if (_previousContact && _previousContact != (hit.collider != null ? hit.collider.transform : hit.transform))
            {
                var args = new InteractionArgs
                {
                    InteractionSource = source,
                    SourceHolder = SourceHolder,
                    Distance = 0f,
                    Target = _previousContact
                };
                DeFocus(args);
                _previousContact = null;
            }

            if (hasHit && _previousContact != (hit.collider != null ? hit.collider.transform : hit.transform))
            {
                var args = new InteractionArgs
                {
                    InteractionSource = source,
                    SourceHolder = SourceHolder,
                    Distance = hit.distance,
                    Target = hit.collider!.transform
                };
                
                Focus(args);
                _previousContact = hit.collider.transform;
            }

            if (hasHit && useInteract && interactInput.Handle())
            {
                var args = new InteractionArgs
                {
                    InteractionSource = source,
                    SourceHolder = SourceHolder,
                    Distance = hit.distance,
                    Target = hit.collider!.transform
                };
                Interact(args);
            }

            if (hasHit && useInteractHold && interactHoldInput.Handle())
            {
                var args = new InteractionArgs
                {
                    InteractionSource = source,
                    SourceHolder = SourceHolder,
                    Distance = hit.distance,
                    Target = hit.collider!.transform
                };
                InteractHold(args);
            }

            if (hasHit && useAfterInteract && afterInteractInput.Handle())
            {
                var args = new InteractionArgs
                {
                    InteractionSource = source,
                    SourceHolder = SourceHolder,
                    Distance = hit.distance,
                    Target = hit.collider!.transform
                };
                AfterInteract(args);
            }

            // if (!hasHit)
            // {
            //     _previousContact = null;
            // }
        }
    }
}