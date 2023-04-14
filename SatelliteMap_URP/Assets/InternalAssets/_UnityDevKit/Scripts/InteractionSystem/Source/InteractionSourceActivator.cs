using System.Collections;
using MyBox;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Source
{
    [RequireComponent(typeof(BaseInteractionSource))]
    public class InteractionSourceActivator : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField, PositiveValueOnly] private float maxRayDistance = 15f;
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField, PositiveValueOnly] private int frameCountFilter = 6;
        
        private BaseInteractionSource _interactionSource;
        private bool _isActivated;
        
        private void Awake()
        {
            _interactionSource = GetComponent<BaseInteractionSource>();
        }

        private void Start()
        {
            Deactivate();
        }

        private void Update()
        {
            if (Time.frameCount % frameCountFilter != 0) return;
            
            var hasHit = Physics.Raycast(origin.position, origin.forward, out _, maxRayDistance, layerMask);

            switch (hasHit)
            {
                case true when !_isActivated:
                    Activate();
                    break;
                case false when _isActivated:
                    Deactivate();
                    break;
            }
        }

        private void Activate()
        {
            StopAllCoroutines();
            _interactionSource.Activate();
            _isActivated = true;
        }

        private void Deactivate()
        {
            StartCoroutine(DelayedDeactivate());
        }

        private IEnumerator DelayedDeactivate()
        {
            const float delay = 0.3f;
            yield return new WaitForSeconds(delay);
            _interactionSource.Deactivate();
            _isActivated = false;
        }
    }
}