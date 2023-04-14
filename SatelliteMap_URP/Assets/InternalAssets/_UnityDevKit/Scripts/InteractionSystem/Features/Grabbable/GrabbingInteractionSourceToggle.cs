using UnityDevKit.InteractionSystem.Source;
using UnityDevKit.InteractionSystem.Source.Extensions;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Features.Grabbable
{
    [RequireComponent(typeof(InteractionSourceGrabbing))]
    public class GrabbingInteractionSourceToggle : MonoBehaviour
    {
        [SerializeField] private BaseInteractionSource interactionSource;

        private InteractionSourceGrabbing _sourceGrabbing;

        private void Awake()
        {
            _sourceGrabbing = GetComponent<InteractionSourceGrabbing>();
        }

        private void Start()
        {
            _sourceGrabbing.OnGrabEnd.AddListener(() => interactionSource.enabled = false);
            _sourceGrabbing.OnThrowEnd.AddListener(() => interactionSource.enabled = true);
        }
    }
}