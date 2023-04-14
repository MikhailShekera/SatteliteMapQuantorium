using MyBox;
using UnityDevKit.Events;
using UnityEngine;

namespace UnityDevKit.Laser
{
    public class Laser : MonoBehaviour
    {
        [Separator("Laser settings")] 
        [SerializeField] private Transform origin;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float maxRayDistance = 100f;
        [SerializeField, DisplayInspector] private LaserData laserData;

        private readonly EventHolder<RaycastHit> _onProcess = new EventHolder<RaycastHit>();

        private GameObject _laser;
        private Transform _laserTransform;
        private MeshRenderer _laserRenderer;
        
        private bool _isWorking;
        private Transform _previousContact;
        private float _currentDistance;
        
        private void Start()
        {
            _laser = Instantiate(laserData.Prefab);
            _laserTransform = _laser.transform;
            _laserTransform.parent = origin;
            _laserTransform.localEulerAngles = Vector3.zero;
            _laserRenderer = _laser.GetComponent<MeshRenderer>();
            _laserRenderer.material = laserData.CommonMaterial;
            
            Hide();
        }

        private void Update()
        {
            if (_isWorking)
            {
                var hasHit = Physics.Raycast(
                    origin.position, 
                    origin.forward, 
                    out var hit, 
                    maxRayDistance, 
                    layerMask);
                _currentDistance = hasHit ? hit.distance : maxRayDistance;
                Process(hasHit, hit);
                DrawLaser(_currentDistance);
            }
        }

        public void Show()
        {
            _laser.SetActive(true);
            _isWorking = true;
        }

        public void Hide()
        {
            _laser.SetActive(false);
            _isWorking = false;
        }
        
        public void Toggle(bool activate, float distance)
        {
            _laser.SetActive(activate);
            if (activate)
            {
                DrawLaser(distance);
            }
        }

        public void Increase()
        {
            // TODO
        }

        public void Decrease()
        {
            // TODO
        }
        
        private void Process(bool hasHit, RaycastHit hitInfo)
        {
            if (hasHit)
            {
                _onProcess.Invoke(hitInfo);
            }
        }

        private void DrawLaser(float distance)
        {
            _laserTransform.localPosition = new Vector3(0f, 0f, distance / 2f);
            var localScale = _laserTransform.localScale;
            localScale = new Vector3(localScale.x, localScale.y, distance);
            _laserTransform.localScale = localScale;
        }
    }
}