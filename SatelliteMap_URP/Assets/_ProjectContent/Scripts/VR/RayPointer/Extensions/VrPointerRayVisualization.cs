using MyBox;
using UnityDevKit.Laser;
using UnityEngine;

namespace SatelliteMap.VR.RayPointer.Extensions
{
    public class VrPointerRayVisualization : VrRayPointerExtension
    {
        [SerializeField, DisplayInspector] private LaserData laserData;

        private GameObject _laser;
        private Transform _laserTransform;
        private MeshRenderer _laserRenderer;
        
        private void Start()
        {
            _laser = Instantiate(laserData.Prefab);
            _laserTransform = _laser.transform;
            _laserTransform.parent = RayPointer.LaserOrigin;
            _laserTransform.localEulerAngles = Vector3.zero;
            HideLaser();

            _laserRenderer = _laser.GetComponent<MeshRenderer>();
            _laserRenderer.material = laserData.CommonMaterial;
        }

        protected override void OnRaycast(bool hasHit, float distance)
        {
            base.OnRaycast(hasHit, distance);
            ShowLaser(distance);
        }

        protected override void OnPointerDeactivated()
        {
            base.OnPointerDeactivated();
            HideLaser();
        }

        protected override void OnPointerClickHold(bool isHolding)
        {
            base.OnPointerClickHold(isHolding);
            if (isHolding)
            {
                SetClickLaser();
            }
            else
            {
                SetCommonLaser();
            }
        }

        #region Laser

        private void ShowLaser(float distance)
        {
            _laser.SetActive(true);
            _laserTransform.localPosition = new Vector3(0f, 0f, distance / 2f);
            var localScale = _laserTransform.localScale;
            localScale = new Vector3(localScale.x, localScale.y, distance);
            _laserTransform.localScale = localScale;
        }

        private void HideLaser()
        {
            _laser.SetActive(false);
        }

        private void SetClickLaser()
        {
            _laser.transform.localScale = new Vector3(laserData.ClickThickness, laserData.ClickThickness, _laser.transform.localScale.z);
            _laserRenderer.sharedMaterial = laserData.ClickMaterial;
        }

        private void SetCommonLaser()
        {
            _laser.transform.transform.localScale = new Vector3(laserData.Thickness, laserData.Thickness, _laser.transform.localScale.z);
            _laserRenderer.sharedMaterial = laserData.CommonMaterial;
        }

        #endregion
    }
}