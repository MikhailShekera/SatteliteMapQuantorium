using MyBox;
using UnityDevKit.InteractionSystem.Core.Args;
using UnityDevKit.Laser;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Source.Extensions
{
    public class RayInteractionSourceLaserVisualization : RayInteractionSourceExtension
    {
        [SerializeField, DisplayInspector] private LaserData laserData;

        private bool _isShowing;
        private GameObject _laser;
        private Transform _laserTransform;
        private MeshRenderer _laserRenderer;
        
        protected override void Init()
        {
            base.Init();
            _laser = Instantiate(laserData.Prefab);
            _laserTransform = _laser.transform;
            _laserTransform.parent = InteractionSource.Origin;
            _laserTransform.localEulerAngles = Vector3.zero;
            HideLaser();

            _laserRenderer = _laser.GetComponent<MeshRenderer>();
            _laserRenderer.material = laserData.CommonMaterial;

            InteractionSource.OnRaycast.AddListener(data => DrawLaser(data.Distance));
        }

        protected override void OnActivateStateChangedAction(bool isActivated)
        {
            base.OnActivateStateChangedAction(isActivated);
            if (!isActivated)
            {
                HideLaser();
            }
        }

        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            ShowLaser();
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            HideLaser();
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractHoldAction(args);
            SetClickLaser();
        }

        protected override void OnAfterInteractAction(InteractionArgs args)
        {
            base.OnAfterInteractAction(args);
            SetCommonLaser();
        }

        #region Laser

        private void ShowLaser()
        {
            _laser.SetActive(true);
            _isShowing = true;
        }
        
        private void HideLaser()
        {
            _laser.SetActive(false);
            _isShowing = false;
        }

        private void DrawLaser(float distance)
        {
            if (!_isShowing) return;
            _laserTransform.localPosition = new Vector3(0f, 0f, distance / 2f);
            var localScale = _laserTransform.localScale;
            localScale = new Vector3(localScale.x, localScale.y, distance);
            _laserTransform.localScale = localScale;
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