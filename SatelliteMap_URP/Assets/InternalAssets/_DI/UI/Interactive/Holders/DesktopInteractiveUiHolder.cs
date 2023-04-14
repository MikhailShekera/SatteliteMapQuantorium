using System.Collections;
using UnityDevKit.Player.UI;
using UnityEngine;

namespace DI.UI.Interactive.Holders
{
    public class DesktopInteractiveUiHolder : InteractiveHolder
    {
        [SerializeField] protected GameObject uiInputHolder;
        [SerializeField] private PlayerUiPointerInputModule uiPointerInputModule;

        private IEnumerator Start()
        {
            while (!uiPointerInputModule)
            {
                yield return null;
                uiPointerInputModule = uiInputHolder.GetComponentInChildren<PlayerUiPointerInputModule>();
            }
        }

        public override void AddCanvas(GameObject canvasObject)
        {
            if (canvasObject.TryGetComponent(out Canvas canvas))
            {
                uiPointerInputModule.AddCanvas(canvas);
            }
        }

        public override void RemoveCanvas(GameObject canvasObject)
        {
            if (canvasObject.TryGetComponent(out Canvas canvas))
            {
                uiPointerInputModule.RemoveCanvas(canvas);
            }
        }
    }
}