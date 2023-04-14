using HurricaneVR.Framework.Core.UI;
using UnityEngine;

namespace DI.UI.Interactive.Holders
{
    public class VrInteractiveUiHolder : InteractiveHolder
    {
        [SerializeField] private HVRInputModule hvrInputModule;

        public HVRInputModule HvrInputModule => hvrInputModule;
        
        public override void AddCanvas(GameObject canvasObject)
        {
            if (canvasObject.TryGetComponent(out Canvas canvas))
            {
                hvrInputModule.AddCanvas(canvas);
            }
        }

        public override void RemoveCanvas(GameObject canvasObject)
        {
            if (canvasObject.TryGetComponent(out Canvas canvas))
            {
                hvrInputModule.RemoveCanvas(canvas);
            }
        }
    }
}