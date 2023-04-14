using MyBox;
using UnityEngine;

namespace SatelliteMap.VR.RayPointer
{
    [RequireComponent(typeof(VrRayPointer))]
    public class VrRayPointerActivator : MonoBehaviour
    {
        [SerializeField, PositiveValueOnly] private float maxRayDistance = 15f;
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField, PositiveValueOnly] private int frameCountFilter = 6;
        
        private VrRayPointer _rayPointer;
        private Transform _laserOrigin;
        private bool _isActivated;
        
        private void Awake()
        {
            _rayPointer = GetComponent<VrRayPointer>();
            _laserOrigin = _rayPointer.LaserOrigin;
        }

        private void Update()
        {
            if (Time.frameCount % frameCountFilter != 0) return;
            
            var hasHit = Physics.Raycast(_laserOrigin.position, _laserOrigin.forward, out _, maxRayDistance, layerMask);

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
            _rayPointer.Show();
            _isActivated = true;
        }

        private void Deactivate()
        {
            _rayPointer.Hide();
            _isActivated = false;
        }
    }
}