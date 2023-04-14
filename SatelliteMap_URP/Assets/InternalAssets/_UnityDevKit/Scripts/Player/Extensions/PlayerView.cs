using System.Collections.Generic;
using MyBox;
using UnityDevKit.Interactable.PlayerView;
using UnityEngine;

namespace UnityDevKit.Player.Extensions
{
    public class PlayerView : PlayerExtension
    {
        [Header("View settings")] 
        [SerializeField] private float distance = 3.5f;
        
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField] private bool useTagFilter = false;
        [SerializeField] [ConditionalField(nameof(useTagFilter))]
        private List<string> allowedTags;

        private Camera _mainCamera;
        private Transform _mainCameraTransform;
        private ViewObject _currentViewObject;
        
        protected override void Start()
        {
            base.Start();
            _mainCamera = Camera.main;
            _mainCameraTransform = _mainCamera.transform;
        }

        protected override void PlayerLoop()
        {
            base.PlayerLoop();
            const int interactingFrameDelay = 10;
            if (Time.frameCount % interactingFrameDelay == 0)
            {
                Searching();
            }
        }

        private void Searching()
        {
            var ray = new Ray(_mainCameraTransform.position, _mainCameraTransform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.magenta);

            if (Physics.Raycast(ray, out var hitInfo, distance, layerMask))
            {
                if (!useTagFilter || allowedTags.Contains(hitInfo.collider.tag))
                {
                    var viewObject = hitInfo.collider.GetComponent<ViewObject>();
                    if (viewObject != null)
                    {
                        DeFocus();
                        Focus(viewObject);
                        return;
                    }
                }
            }
            DeFocus();
        }

        private void Focus(ViewObject viewObject)
        {
            _currentViewObject = viewObject;
            _currentViewObject.Focus();
        }

        private void DeFocus()
        {
            if (_currentViewObject != null)
            {
                _currentViewObject.DeFocus();
                _currentViewObject = null;
            }
        }
    }
}