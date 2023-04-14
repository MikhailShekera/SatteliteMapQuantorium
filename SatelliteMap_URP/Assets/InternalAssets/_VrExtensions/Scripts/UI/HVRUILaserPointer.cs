//using DI.UI.Interactive.Holders;
using HurricaneVR.Framework.Core.UI;
using UnityEngine;
//using Zenject;

namespace VrExtensions.UI
{
    [RequireComponent(typeof(UnityDevKit.Laser.Laser))]
    public class HVRUILaserPointer : HVRUIPointer
    {
        private UnityDevKit.Laser.Laser _laser;
        private bool _isInitialized;
        
        // [Inject]
        // public void Construct(IInteractiveHolder interactiveHolder)
        // {
        //     InputModule = ((VrInteractiveUiHolder) interactiveHolder).HvrInputModule;
        //     InputModule!.AddPointer(this);
        // }
        
        private void Awake()
        {
            _laser = GetComponent<UnityDevKit.Laser.Laser>();

            _isInitialized = InputModule != null;
        }

        protected override void Update()
        {
            if (!_isInitialized && !InputModule)
            {
                InputModule = FindObjectOfType<HVRInputModule>();
                InputModule.AddPointer(this);
            }
            else
            {
                _isInitialized = true;
                var distance = Vector3.Distance(transform.position, PointerEventData.pointerCurrentRaycast.worldPosition);
                _laser.Toggle(CurrentUIElement, distance); // TODO -- toggle with distance
            }
        }
    }
}